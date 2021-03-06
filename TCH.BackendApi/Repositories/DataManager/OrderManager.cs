using System.Drawing;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using TCH.BackendApi.EF;
using TCH.BackendApi.Repositories.DataRepository;
using TCH.Data.Entities;
using TCH.Utilities.Claims;
using TCH.Utilities.Enum;
using TCH.Utilities.Error;
using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.Utilities.SubModels;
using TCH.ViewModel.RequestModel;
using TCH.ViewModel.SubModels;

namespace TCH.BackendApi.Repositories.DataManager;

public class OrderManager : IOrderRepository, IDisposable
{
    private readonly APIContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string? _userId;
    private readonly IStorageService _storageService;
    private readonly string _accessToken;
    private readonly IMapper _mapper;

    public OrderManager(APIContext context, IHttpContextAccessor httpContext, IMapper mapper,
        IStorageService storageService)
    {
        _context = context;
        _httpContextAccessor = httpContext;
        _mapper = mapper;
        _storageService = storageService;
        _userId = httpContext?.HttpContext?.User.FindFirst(ClaimValue.ID)?.Value;
        _accessToken = httpContext?.HttpContext != null ? httpContext.HttpContext.Request.Headers["Authorization"] : "";
    }

    public async Task<Respond<object>> Create(OrderRequest request)
    {
        var branch = await _context.Branches.FindAsync(request.BranchID);
        if (branch == null)
            return new Respond<object>
            {
                Result = 0,
                Message = "Không có thông tin nhánh",
            };
        double subTotal = 0;
        foreach (var item in request.OrderItems)
        {
            subTotal += (item.PriceProduct + item.PriceSize) * item.Quantity;
            foreach (var topping in item.Toppings)
            {
                var toppingDb = await _context.Toppings.FindAsync(topping.ToppingID);
                if (toppingDb != null)
                {
                    subTotal += toppingDb.SubPrice * topping.Quantity;
                }
            }
        }

        var totalAmount = subTotal - (request.ReducePromotion + request.ReduceAmount);
        if (!string.IsNullOrWhiteSpace(request.CustomerID))
        {
            var customer = await _context.Customers
                .Include(x => x.Bean)
                .FirstOrDefaultAsync(x => x.ID == request.CustomerID);
            if (customer != null)
            {
                if (totalAmount >= customer.Bean.ConversationMoney)
                {
                    switch (customer.Bean.Code)
                    {
                        case BeanType.New:
                            customer.Point += (int) (totalAmount / customer.Bean.ConversationMoney) *
                                              customer.Bean.ConversationPoint;
                            break;
                        case BeanType.Bronze:
                            customer.Point += (int) (totalAmount / customer.Bean.ConversationMoney) *
                                              customer.Bean.ConversationPoint;
                            break;
                        case BeanType.Silver:
                            customer.Point += (int) (totalAmount / customer.Bean.ConversationMoney) *
                                              customer.Bean.ConversationPoint;
                            break;
                        case BeanType.Gold:
                            customer.Point += (int) (totalAmount / customer.Bean.ConversationMoney) *
                                              customer.Bean.ConversationPoint;
                            break;
                        case BeanType.Diamond:
                            customer.Point += (int) (totalAmount / customer.Bean.ConversationMoney) *
                                              customer.Bean.ConversationPoint;
                            break;
                        default:
                            customer.Point += (int) (totalAmount / customer.Bean.ConversationMoney) *
                                              customer.Bean.ConversationPoint;
                            break;
                    }
                }
            }
        }

        var orderRe = new Order
        {
            ID = Guid.NewGuid().ToString(),
            TableNum = request.TableNum,
            Cashier = request.Cashier,
            Code = "ORDER.N0" + DateTime.Now.Minute + "-" + DateTime.Now.Day,
            SubAmount = subTotal,
            TotalAmount = subTotal - (request.ReducePromotion + request.ReduceAmount),
            Status = request.OrderType == OrderType.Shipping ? OrderStatus.Open : OrderStatus.Finished,
            OrderType = request.OrderType,
            ReducePromotion = request.ReducePromotion,
            ReduceAmount = request.ReduceAmount,
            CustomerPut = request.CustomerPut,
            CustomerReceive = request.CustomerPut - (subTotal - request.ReducePromotion - request.ReduceAmount),
            ShippingFee = request.ShippingFee,
            CreateDate = DateTime.Now,
            Description = request.Description,
            CancellationReason = null,
            UserCreateID = _userId,
            PaymentType = request.PaymentType,
            CustomerID = request.CustomerID,
            BranchID = request.BranchID,
        };
        await _context.Orders.AddAsync(orderRe);
        var orderDetails = new List<OrderDetail>();
        foreach (var item in request.OrderItems)
        {
            var productDb = await _context.Products.FindAsync(item.ProductID);
            var sizeDb = await _context.Sizes.FindAsync(item.SizeID);
            if (productDb == null || sizeDb == null)
            {
                return new Respond<object>
                {
                    Result = 1,
                    Message = "Không tìm thấy product hoặc size, hãy chọn lại",
                };
            }

            if (item.Toppings.Count > 0)
            {
                var orderToppingDetail = new List<OrderToppingDetail>();
                foreach (var topping in item.Toppings)
                {
                    var toppingDb = await _context.Toppings.FindAsync(topping.ToppingID);
                    if (toppingDb != null)
                    {
                        orderToppingDetail.Add(new OrderToppingDetail
                        {
                            ToppingID = topping.ToppingID,
                            OrderID = orderRe.ID,
                            ProductID = productDb.ID,
                            SubPrice = toppingDb.SubPrice,
                            Name = toppingDb.Name,
                            Quantity = topping.Quantity,
                        });
                    }
                }

                await _context.OrderToppingDetails.AddRangeAsync(orderToppingDetail);

                var orderDetail = new OrderDetail
                {
                    OrderID = orderRe.ID,
                    Quantity = item.Quantity,
                    PriceProduct = item.PriceProduct,
                    Description = item.Description,
                    SugarType = item.SugarType,
                    IcedType = item.IcedType,
                    PriceSize = item.PriceSize,
                    SizeID = sizeDb.ID,
                    ProductID = productDb.ID,
                    OrderToppingDetails = orderToppingDetail,
                };
                orderDetails.Add(orderDetail);
            }
            else
            {
                orderDetails.Add(new OrderDetail
                {
                    OrderID = orderRe.ID,
                    Quantity = item.Quantity,
                    PriceProduct = item.PriceProduct,
                    Description = item.Description,
                    SugarType = item.SugarType,
                    PriceSize = item.PriceSize,
                    SizeID = item.SizeID,
                    ProductID = productDb.ID,
                });
            }
        }

        //trừ nguyên liệu trong kho
        var data = new List<MassMaterial>();
        foreach (var item in orderDetails)
        {
            var product = await _context.Products.FindAsync(item.ProductID);
            var recipes = await _context
                .RecipeDetails
                .Where(x =>
                    x.ProductID == item.ProductID
                    && x.SizeID == item.SizeID)
                .ToListAsync();
            if (recipes != null)
            {
                foreach (var recipe in recipes)
                {
                    var stockMaterial = await _context
                        .StockMaterials
                        .Include(x => x.Material)
                        .Include(x => x.Measure)
                        .FirstOrDefaultAsync(x =>
                            x.MaterialID == recipe.MaterialID
                            && x.BranchID == request.BranchID);
                    if (stockMaterial == null
                        || (stockMaterial.StandardMass == 0 ||
                            stockMaterial.StandardMass - recipe.Weight * item.Quantity < 0))
                    {
                        return new Respond<object>
                        {
                            Data = orderRe,
                            Result = 0,
                            Message =
                                $"Nguyên liệu {stockMaterial?.Material?.Name} trong chi nhánh {branch.Name} của sản phẩm {product.Name} không đủ hoặc đã hết."
                        };
                    }

                    stockMaterial.StandardMass -= recipe.Weight * item.Quantity;
                    stockMaterial.Mass = stockMaterial.StandardMass / stockMaterial.Measure.ConversionFactor;
                    _context.StockMaterials.Update(stockMaterial);
                }
            }
        }

        _context.OrderDetails.AddRange(orderDetails);
        await _context.SaveChangesAsync();
        return new Respond<object>
        {
            Data = orderRe,
            Result = 1,
            Message = "Tạo thành công"
        };
    }

    public async Task<string> PrintInvoicePaymented(string invoiceId)
    {
        var invoice = await _context.Orders
            .Include(x => x.OrderDetails)
            .ThenInclude(x => x.OrderToppingDetails)
            .ThenInclude(x => x.Topping)
            .FirstOrDefaultAsync(x => x.ID == invoiceId);
        if (invoice == null) throw new CustomException("Không tìm thấy thông tin hóa đơn");
        var branch = await _context.Branches.FirstOrDefaultAsync(x => x.ID == invoice.BranchID);
        if (branch == null) throw new CustomException("Không tìm thấy thông tin cửa hàng");
        string newPathXML = Path.Combine(Directory.GetCurrentDirectory(), "LayoutInvoice",
            "Type" + (branch.LayoutType + 1), "DefaultTemplate.xml");
        string newPathXSLT = Path.Combine(Directory.GetCurrentDirectory(), "LayoutInvoice",
            "Type" + (branch.LayoutType + 1), "DefaultTemplate.xslt");

        XmlDocument doc = new XmlDocument();
        doc.Load(newPathXML);

        if (3 != branch.LayoutType)
        {
            int scale = 30;

            XmlElement InvoiceRestaurantScale = (XmlElement) doc.SelectSingleNode("/Invoice/Scale");
            InvoiceRestaurantScale.InnerText = scale.ToString();
        }

        XmlElement InvoiceRestaurantTitle = (XmlElement) doc.SelectSingleNode("/Invoice/Title");
        if (string.IsNullOrEmpty(branch.TitleInvoice))
        {
            branch.TitleInvoice = @"THE COFFEE HOUSE";
        }

        InvoiceRestaurantTitle.InnerText = (branch.TitleInvoice).ToUpper();

        XmlElement InvoiceRestaurantLogo = (XmlElement) doc.SelectSingleNode("/Invoice/Restaurant/Logo");
        if (string.IsNullOrEmpty(branch.LogoInvoice))
        {
            branch.LogoInvoice =
                @"data:image/png;base64,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";
        }

        InvoiceRestaurantLogo.InnerText = branch.LogoInvoice;
        XmlElement InvoiceRestaurantName = (XmlElement) doc.SelectSingleNode("/Invoice/Restaurant/Name");
        InvoiceRestaurantName.InnerText = branch.Name;
        XmlElement InvoiceRestaurantAddress = (XmlElement) doc.SelectSingleNode("/Invoice/Restaurant/Address");
        InvoiceRestaurantAddress.InnerText = branch.Adderss;
        XmlElement InvoiceRestaurantPhone = (XmlElement) doc.SelectSingleNode("/Invoice/Restaurant/Phone");
        InvoiceRestaurantPhone.InnerText = branch.Phone;
        XmlElement InvoiceRestaurantHeadercontent =
            (XmlElement) doc.SelectSingleNode("/Invoice/Restaurant/Headercontent");
        InvoiceRestaurantHeadercontent.InnerText = branch.Headercontent ?? "www.thecoffeehouse.com";

        XmlElement InvoiceCodeElement = (XmlElement) doc.SelectSingleNode("/Invoice/InvoiceCode");
        InvoiceCodeElement.InnerText = invoice.Code;

        XmlElement DinnerTableElement = (XmlElement) doc.SelectSingleNode("/Invoice/DinnerTable");
        DinnerTableElement.InnerText = invoice.TableNum != null ? invoice.TableNum.ToString() : null;

        XmlElement CreateDateElement = (XmlElement) doc.SelectSingleNode("/Invoice/CreateDate");
        CreateDateElement.InnerText = invoice.CreateDate.ToString("dd/MM/yyyy hh:mm:ss tt");

        XmlElement UserCreateElement = (XmlElement) doc.SelectSingleNode("/Invoice/UserCreate");
        UserCreateElement.InnerText = invoice.Cashier;


        XmlElement elementDetail = (XmlElement) doc.SelectSingleNode("/Invoice/Detail");
        var orderDetails = await _context
            .OrderDetails
            .Include(x => x.Size)
            .Include(x => x.OrderToppingDetails)
            .Where(x => x.OrderID == invoice.ID)
            .ToListAsync();
        foreach (var item in invoice.OrderDetails)
        {
            // using (var _context = new APIContext())
            // {
            var food = _context.Products.FirstOrDefault(e => e.ID == item.ProductID);
            if (food != null)
            {
                item.Product = food;
                // var toppingDetail = await _context.OrderToppingDetails
                //     .Include(x => x.Topping)
                //     .Where(x => x.OrderID == item.OrderID && x.ProductID == item.ProductID).ToListAsync();

                XmlElement UnitElement = doc.CreateElement("Unit");
                UnitElement.InnerText = "";
                double amountTopping = 0;
                if (item.OrderToppingDetails != null)
                {
                    foreach (var topping in item.OrderToppingDetails)
                    {
                        amountTopping += topping.Quantity * topping.SubPrice;
                        UnitElement.InnerText += Convert.ToString(topping.Topping.Name) + $" x{topping.Quantity},";
                    }
                }

                XmlElement InvoiceDetailElement = doc.CreateElement("InvoiceDetail");

                XmlElement FoodNameElement = doc.CreateElement("FoodName");
                FoodNameElement.InnerText = Convert.ToString(item.Product.Name + "(" + item.Size.Name + ")");
                XmlElement QuantityElement = doc.CreateElement("Quantity");
                QuantityElement.InnerText = Convert.ToString(item.Quantity);
                // XmlElement DecimalFactorElement = doc.CreateElement("DecimalFactor");
                // DecimalFactorElement.InnerText = Convert.ToString(item.PriceProduct);
                XmlElement PriceElement = doc.CreateElement("Price");
                PriceElement.InnerText = Convert.ToString(item.PriceProduct + item.PriceSize + amountTopping);


                XmlElement AmountElement = doc.CreateElement("Amount");
                // AmountElement.InnerText = Convert.ToString(item.Quantity * (item.PriceProduct + item.PriceSize));

                AmountElement.InnerText = ((Convert.ToDecimal(item.PriceProduct) + Convert.ToDecimal(item.PriceSize)
                                               + Convert.ToDecimal(amountTopping)) *
                                           Convert.ToDecimal(item.Quantity)).ToString();

                InvoiceDetailElement.AppendChild(FoodNameElement);
                InvoiceDetailElement.AppendChild(UnitElement);
                InvoiceDetailElement.AppendChild(QuantityElement);
                // InvoiceDetailElement.AppendChild(DecimalFactorElement);
                InvoiceDetailElement.AppendChild(PriceElement);
                InvoiceDetailElement.AppendChild(AmountElement);

                elementDetail.AppendChild(InvoiceDetailElement);
            }
            //}
        }

        XmlElement SumAmountElement = (XmlElement) doc.SelectSingleNode("/Invoice/SumAmount");
        SumAmountElement.InnerText = invoice.SubAmount.ToString();
        XmlElement ReduceAmountElement = (XmlElement) doc.SelectSingleNode("/Invoice/ReduceAmount");
        ReduceAmountElement.InnerText = invoice.ReduceAmount.ToString();
        XmlElement ShippingElement = (XmlElement) doc.SelectSingleNode("/Invoice/ShippingFee");
        ShippingElement.InnerText = invoice.ShippingFee.ToString();
        XmlElement SurchargeMoneyElement = (XmlElement) doc.SelectSingleNode("/Invoice/SurchargeMoney");
        SurchargeMoneyElement.InnerText = invoice.ReduceAmount.ToString();
        XmlElement VATElement = (XmlElement) doc.SelectSingleNode("/Invoice/VAT");
        VATElement.InnerText = "10%";
        XmlElement PromotionElement = (XmlElement) doc.SelectSingleNode("/Invoice/ReducePromotion");
        PromotionElement.InnerText = invoice.ReducePromotion.ToString();

        // Tiếu điểm
        XmlElement MoneyConvertPointElement = (XmlElement) doc.SelectSingleNode("/Invoice/MoneyConvertPoint");
        MoneyConvertPointElement.InnerText = "0";

        XmlElement PaymentElement = (XmlElement) doc.SelectSingleNode("/Invoice/Payment");
        PaymentElement.InnerText = invoice.TotalAmount.ToString();
        XmlElement GuestPutElement = (XmlElement) doc.SelectSingleNode("/Invoice/GuestPut");
        GuestPutElement.InnerText = invoice.CustomerPut.ToString();
        XmlElement GuestRecevieElement = (XmlElement) doc.SelectSingleNode("/Invoice/GuestReceive");
        if ((invoice.CustomerPut - invoice.TotalAmount) > 0)
        {
            GuestRecevieElement.InnerText = (invoice.CustomerPut - invoice.TotalAmount).ToString();
        }
        else
        {
            GuestRecevieElement.InnerText = "0";
        }

        if (!string.IsNullOrEmpty(branch.Footercontent))
        {
            XmlElement InvoiceRestaurantFootercontent = (XmlElement) doc.SelectSingleNode("/Invoice/Footercontent");

            string[] lines = branch.Footercontent.Split('|');

            foreach (var line in lines)
            {
                XmlElement LineElement = doc.CreateElement("Line");
                XmlElement ValueElement = doc.CreateElement("Value");
                ValueElement.InnerText = Convert.ToString(line);
                LineElement.AppendChild(ValueElement);
                InvoiceRestaurantFootercontent?.AppendChild(LineElement);
            }
        }

        XslCompiledTransform xslt = new XslCompiledTransform();
        xslt.Load(newPathXSLT);
        MemoryStream ms = new MemoryStream();
        XmlTextWriter writer = new XmlTextWriter(ms, Encoding.UTF8);
        xslt.Transform(doc, writer);
        ms.Position = 0;
        StreamReader rd = new StreamReader(ms);
        string strHtml = rd.ReadToEnd();
        rd.Close();
        ms.Close();
        return strHtml;
    }

    public Task<MessageResult> Update(OrderRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<Respond<object>> GetAllMoneyByBranchId(string branhID, Search request)
    {
        var queryable = _context
            .Orders
            .Where(x => x.BranchID == branhID);

        if (request.StartDate != null && request.EndDate != null)
            queryable = queryable.Where(x =>
                x.CreateDate <= request.EndDate && x.CreateDate >= request.StartDate);
        //paging
        var moneyAll = await queryable.SumAsync(x => x.TotalAmount);

        return new Respond<object>
        {
            Data = moneyAll,
            Result = 1,
            Message = "Thành công",
        };
    }

    public async Task<Respond<MoneyByDay>> GetChartMoney(Search request)
    {
        var queryable = _context
            .Orders
            .AsQueryable();

        if (request.StartDate != null && request.EndDate != null)
            queryable = queryable.Where(x =>
                request.EndDate != null
                && x.CreateDate.Date <= request.EndDate.Value.Date
                && request.StartDate != null
                && x.CreateDate.Date >= request.StartDate.Value.Date);
        var moneyByDayDetails = await queryable
            .GroupBy(x => x.CreateDate.Date)
            .Select(x => new MoneyByDayDetail()
            {
                DateTime = x.Key.Date,
                Revenue = x.Sum(z => z.TotalAmount),
            })
            .ToListAsync();
        var moneyByday = new MoneyByDay()
        {
            MoneyByDayDetails = moneyByDayDetails,
            Branch = null,
            TotalAmount = moneyByDayDetails.Sum(x => x.Revenue),
        };

        return new Respond<MoneyByDay>
        {
            Data = moneyByday,
            Result = 1,
            Message = "Thành công",
        };
    }

    public async Task<Respond<MoneyByDay>> GetChartMoneyByBranchId(string branhID, Search request)
    {
        var queryable = _context
            .Orders
            .Where(x => x.BranchID == branhID);

        if (request.StartDate != null && request.EndDate != null)
            queryable = queryable.Where(x =>
                request.EndDate != null && x.CreateDate.Date <= request.EndDate.Value.Date &&
                request.StartDate != null && x.CreateDate.Date >= request.StartDate.Value.Date);
        var moneyByDayDetails = await queryable
            .GroupBy(x => x.CreateDate.Date)
            .Select(x => new MoneyByDayDetail()
            {
                DateTime = x.Key.Date,
                Revenue = x.Sum(z => z.TotalAmount),
            })
            .ToListAsync();
        var branch = await _context.Branches.FindAsync(branhID);
        var moneyByDay = new MoneyByDay()
        {
            MoneyByDayDetails = moneyByDayDetails,
            Branch = _mapper.Map<BranchVm>(branch),
            TotalAmount = moneyByDayDetails.Sum(x => x.Revenue),
        };

        return new Respond<MoneyByDay>
        {
            Data = moneyByDay,
            Result = 1,
            Message = "Thành công",
        };
    }

    public async Task<Respond<object>> GetAllMoney(Search request)
    {
        var queryable = _context.Orders.AsQueryable();

        if (request.StartDate != null && request.EndDate != null)
            queryable = queryable.Where(x =>
                x.CreateDate <= request.EndDate && x.CreateDate >= request.StartDate);
        //paging
        var moneyAll = await queryable.SumAsync(x => x.TotalAmount);

        return new Respond<object>
        {
            Data = moneyAll,
            Result = 1,
            Message = "Thành công",
        };
    }

    public async Task<Respond<PagedList<Order>>> GetByBranhID(string branhID, Search request)
    {
        var result = _context
            .Orders
            .Include(x => x.OrderDetails)
            .ThenInclude(e => e.OrderToppingDetails)
            .Where(x => x.BranchID == branhID);
        if (!string.IsNullOrEmpty(request.Name))
            result = result.Where(x => x.Code.Contains(request.Name));
        if (request.StartDate != null && request.EndDate != null)
            result = result.Where(x =>
                request.EndDate != null && x.CreateDate.Date <= request.EndDate.Value.Date &&
                request.StartDate != null && x.CreateDate.Date >= request.StartDate.Value.Date);
        //paging
        int totalRow = await result.CountAsync();
        List<Order> data;
        if (request.IsPging)
        {
            data = await result
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToListAsync();
        }
        else
            data = await result.ToListAsync();

        var pagedResult = new PagedList<Order>
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int) Math.Ceiling((double) totalRow / request.PageSize),
            Items = data,
        };
        return new Respond<PagedList<Order>>
        {
            Data = pagedResult,
            Result = 1,
            Message = "Thành công",
        };
    }

    public async Task<Respond<PagedList<Order>>> GetByUser(string userId, Search request)
    {
        var result = _context
            .Orders
            .Include(x => x.OrderDetails)
            .ThenInclude(e => e.OrderToppingDetails)
            .Where(x => x.UserCreateID == userId);
        if (!string.IsNullOrEmpty(request.Name))
            result = result.Where(x => x.Code.Contains(request.Name));
        if (request.StartDate != null && request.EndDate != null)
            result = result.Where(x =>
                request.EndDate != null && x.CreateDate.Date <= request.EndDate.Value.Date &&
                request.StartDate != null && x.CreateDate.Date >= request.StartDate.Value.Date);
        //paging
        int totalRow = await result.CountAsync();
        List<Order> data;
        if (request.IsPging)
        {
            data = await result
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToListAsync();
        }
        else
            data = await result.ToListAsync();

        var pagedResult = new PagedList<Order>
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int) Math.Ceiling((double) totalRow / request.PageSize),
            Items = data,
        };
        return new Respond<PagedList<Order>>
        {
            Data = pagedResult,
            Result = 1,
            Message = "Thành công",
        };
    }

    public async Task<Respond<PagedList<Order>>> GetAll(Search request)
    {
        var result = _context
            .Orders
            .Include(x => x.OrderDetails)
            .ThenInclude(e => e.OrderToppingDetails)
            .AsQueryable();
        if (!string.IsNullOrEmpty(request.Name))
            result = result.Where(x => x.Code.Contains(request.Name));
        if (request.StartDate != null && request.EndDate != null)
            result = result.Where(x =>
                request.EndDate != null && x.CreateDate.Date <= request.EndDate.Value.Date &&
                request.StartDate != null && x.CreateDate.Date >= request.StartDate.Value.Date);
        //paging
        int totalRow = await result.CountAsync();
        List<Order> data;
        if (request.IsPging)
        {
            data = result.Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToList();
        }
        else
            data = result.ToList();

        var pagedResult = new PagedList<Order>
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int) Math.Ceiling((double) totalRow / request.PageSize),
            Items = data,
        };
        return new Respond<PagedList<Order>>
        {
            Data = pagedResult,
            Result = 1,
            Message = "Thành công",
        };
    }

    public async Task<Respond<PagedList<OrderInUser>>> CompareOrderUserInBranch(string branchId, Search request)
    {
        var result = _context
            .Orders
            .Include(x => x.Branch)
            .Where(x => x.BranchID == branchId && x.UserCreateID != null);
        if (!string.IsNullOrEmpty(request.Name))
            result = result.Where(x => x.Code.Contains(request.Name));
        if (request.StartDate != null && request.EndDate != null)
            result = result
                .Where(x => request.EndDate != null
                            && x.CreateDate.Date <= request.EndDate.Value.Date
                            && request.StartDate != null
                            && x.CreateDate.Date >= request.StartDate.Value.Date);
        //paging
        int totalRow = await result.GroupBy(x => x.UserCreateID).CountAsync();

        List<OrderInUser> data;
        if (request.IsPging)
        {
            data = result
                .GroupBy(x => x.UserCreateID)
                .Select(x => new OrderInUser()
                {
                    UserCreateID = x.Key,
                    QuantityOrder = x.Select(z => z).Count(),
                    Cashier = x.First().Cashier,
                    CustomerPut = x.Sum(z => z.CustomerPut),
                    CustomerReceive = x.Sum(z => z.CustomerReceive),
                    ReduceAmount = x.Sum(z => z.ReduceAmount),
                    ReducePromotion = x.Sum(z => z.ReducePromotion),
                    ShippingFee = x.Sum(z => z.ShippingFee),
                    SubAmount = x.Sum(z => z.SubAmount),
                    TotalAmount = x.Sum(z => z.TotalAmount),
                    // UserName = _context.Users.Find(x.First().UserCreateID)!.UserName,
                })
                .OrderByDescending(x => x.TotalAmount)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToList();
        }
        else
            data = result.GroupBy(x => x.UserCreateID)
                .Select(x => new OrderInUser()
                {
                    UserCreateID = x.Key,
                    Cashier = x.First().Cashier,
                    QuantityOrder = x.Select(z => z).Count(),
                    CustomerPut = x.Sum(z => z.CustomerPut),
                    CustomerReceive = x.Sum(z => z.CustomerReceive),
                    ReduceAmount = x.Sum(z => z.ReduceAmount),
                    ReducePromotion = x.Sum(z => z.ReducePromotion),
                    ShippingFee = x.Sum(z => z.ShippingFee),
                    SubAmount = x.Sum(z => z.SubAmount),
                    TotalAmount = x.Sum(z => z.TotalAmount),
                    // UserName = _context.Users.Find(x.First().UserCreateID)!.UserName,
                })
                .OrderByDescending(x => x.TotalAmount)
                .ToList();

        var pagedResult = new PagedList<OrderInUser>
        {
            TotalRecord = totalRow,
            PageSize = request.PageSize,
            CurrentPage = request.PageNumber,
            TotalPages = (int) Math.Ceiling((double) totalRow / request.PageSize),
            Items = data,
        };
        return new Respond<PagedList<OrderInUser>>
        {
            Data = pagedResult,
            Result = 1,
            Message = "Thành công",
        };
    }

    public async Task<string> ExcelAllOrder(Search request)
    {
        var result = _context
            .Orders
            .Include(x => x.Branch)
            .OrderBy(x => x.CreateDate)
            .AsQueryable();
        if (!string.IsNullOrEmpty(request.Name))
            result = result.Where(x => x.Code.Contains(request.Name));
        if (request.StartDate != null && request.EndDate != null)
            result = result
                .Where(x => request.EndDate != null
                            && x.CreateDate.Date <= request.EndDate.Value.Date
                            && request.StartDate != null
                            && x.CreateDate.Date >= request.StartDate.Value.Date);
        var moneyAllBranch = await result.SumAsync(x => x.TotalAmount);
        //paging
        var data = await result
            .GroupBy(x => x.BranchID)
            .Select(x => new
            {
                Branch = x.Key,
                Orders = x.Select(z => z).ToList(),
            }).ToListAsync();

        CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN"); // try with "en-US"
        var stream = new MemoryStream();
        using (var xlPackage = new ExcelPackage(stream))
        {
            var worksheet = xlPackage.Workbook.Worksheets.Add("Bao-cao");
            var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
            namedStyle.Style.Font.UnderLine = true;
            namedStyle.Style.Font.Color.SetColor(Color.Blue);
            const int startRow = 5;
            var row = startRow;

            //Create Headers and format them
            worksheet.Cells["A1"].Value = "Báo cáo doanh số bán hàng cho tất cả cửa hàng";
            using (var r = worksheet.Cells["A1:N1"])
            {
                r.Merge = true;
                r.Style.Font.Size = 28;
                r.Style.Font.Color.SetColor(Color.White);
                r.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
            }

            worksheet.Cells["A2"].Value = "Ngày báo cáo: " + DateTime.Now.ToShortDateString();
            using (var r = worksheet.Cells["A2:N2"])
            {
                r.Merge = true;
                r.Style.Font.Size = 20;
                r.Style.Font.Color.SetColor(Color.White);
                r.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(24, 220, 255));
            }

            worksheet.Cells["A5"].Value = "STT";
            worksheet.Cells["B5"].Value = "Tên chi nhánh";
            worksheet.Cells["C5"].Value = "Địa chỉ";
            worksheet.Cells["D5"].Value = "Thành phố";
            worksheet.Cells["E5"].Value = "Số điện thoại";
            worksheet.Cells["F5"].Value = "Từ ngày";
            worksheet.Cells["G5"].Value = "Đến ngày";
            worksheet.Cells["H5"].Value = "Giảm trừ";
            worksheet.Cells["I5"].Value = "Giảm giá theo khuyến mãi";
            worksheet.Cells["J5"].Value = "Tiền khách hàng nhận";
            worksheet.Cells["K5"].Value = "Tiền khách hàng đưa";
            worksheet.Cells["L5"].Value = "Phí vận chuyển";
            worksheet.Cells["M5"].Value = "Tổng tiền";
            worksheet.Cells["N5"].Value = "Thành tiền";
            worksheet.Cells["A5:N5"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells["A5:N5"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
            worksheet.Cells["A5:N5"].Style.Font.Bold = true;
            row = 6;
            foreach (var item in data.Select((value, i) => new {value, i}))
            {
                var customer = new Customer();
                worksheet.Cells[row, 1].Value = item.i + 1;
                worksheet.Cells[row, 2].Value = item.value.Orders[0].Branch.Name;
                worksheet.Cells[row, 3].Value = item.value.Orders[0].Branch.Adderss;
                worksheet.Cells[row, 4].Value = item.value.Orders[0].Branch.City;
                worksheet.Cells[row, 5].Value = item.value.Orders[0].Branch.Phone;
                worksheet.Cells[row, 6].Value = request.StartDate?.ToShortDateString();
                worksheet.Cells[row, 7].Value = request.EndDate?.ToShortDateString();
                worksheet.Cells[row, 8].Value =
                    item.value.Orders.Sum(x => x.ReduceAmount).ToString("#,###", cul.NumberFormat);
                worksheet.Cells[row, 9].Value =
                    item.value.Orders.Sum(x => x.ReducePromotion).ToString("#,###", cul.NumberFormat);
                worksheet.Cells[row, 10].Value =
                    item.value.Orders.Sum(x => x.CustomerReceive).ToString("#,###", cul.NumberFormat);
                worksheet.Cells[row, 11].Value =
                    item.value.Orders.Sum(x => x.CustomerPut).ToString("#,###", cul.NumberFormat);
                worksheet.Cells[row, 12].Value =
                    item.value.Orders.Sum(x => x.ShippingFee).ToString("#,###", cul.NumberFormat);
                worksheet.Cells[row, 13].Value =
                    item.value.Orders.Sum(x => x.SubAmount).ToString("#,###", cul.NumberFormat);
                worksheet.Cells[row, 14].Value =
                    item.value.Orders.Sum(x => x.TotalAmount).ToString("#,###", cul.NumberFormat);
                row++;
            }

            row++;
            worksheet.Cells[$"A{row}"].Value = "Doanh thu bán hàng";
            using (var r = worksheet.Cells[$"A{row}:M{row}"])
            {
                r.Merge = true;
                r.Style.Font.Size = 14;
                r.Style.Font.Color.SetColor(Color.Black);
                r.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(223, 249, 251));
            }

            worksheet.Cells[$"N{row}"].Value = moneyAllBranch.ToString("#,###", cul.NumberFormat);
            using (var r = worksheet.Cells[$"N{row}:N{row}"])
            {
                r.Merge = true;
                r.Style.Font.Size = 14;
                r.Style.Font.Color.SetColor(Color.White);
                r.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(116, 185, 255));
            }

            // set some core property values
            xlPackage.Workbook.Properties.Title = "Báo cáo";
            xlPackage.Workbook.Properties.Author = "Quản lý chi nhánh";
            xlPackage.Workbook.Properties.Subject = "Báo cáo";
            // save the new spreadsheet
            xlPackage.Save();
            // Response.Clear();
        }

        stream.Position = 0;
        var fileName = "temp-order.xlsx";
        await _storageService.SaveFileAsync(stream, fileName);
        return _storageService.GetPathBE(fileName);
    }

    public async Task<string> ExcelAllOrderByBranchID(string branchId, Search request)
    {
        var result = await _context
            .Orders
            .Include(x => x.Branch)
            .OrderBy(x => x.CreateDate)
            .Where(x => x.BranchID == branchId)
            .ToListAsync();
        if (!string.IsNullOrEmpty(request.Name))
            result = result.Where(x => x.Code.Contains(request.Name)).ToList();
        if (request.StartDate != null && request.EndDate != null)
            result = result
                .Where(x => x.CreateDate.Date <= request.EndDate?.Date
                            && x.CreateDate.Date >= request.StartDate?.Date)
                .ToList();
        //paging
        int totalRow = result.Count();
        var data = new List<Order>();
        if (request.IsPging)
        {
            data = result.Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToList();
        }
        else
            data = result.ToList();

        CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN"); // try with "en-US"
        string a = double.Parse("12345").ToString("#,###", cul.NumberFormat);
        var stream = new MemoryStream();
        using (var xlPackage = new ExcelPackage(stream))
        {
            var worksheet = xlPackage.Workbook.Worksheets.Add("Bao-cao");
            var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
            namedStyle.Style.Font.UnderLine = true;
            namedStyle.Style.Font.Color.SetColor(Color.Blue);
            const int startRow = 5;
            var row = startRow;

            //Create Headers and format them
            worksheet.Cells["A1"].Value = "Báo cáo bán hàng của cửa hàng";
            using (var r = worksheet.Cells["A1:Q1"])
            {
                r.Merge = true;
                r.Style.Font.Size = 28;
                r.Style.Font.Color.SetColor(Color.White);
                r.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
            }

            worksheet.Cells["A2"].Value = "Ngày báo cáo: " + DateTime.Now.ToShortDateString();
            using (var r = worksheet.Cells["A2:Q2"])
            {
                r.Merge = true;
                r.Style.Font.Size = 20;
                r.Style.Font.Color.SetColor(Color.White);
                r.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(24, 220, 255));
            }

            worksheet.Cells["A5"].Value = "STT";
            worksheet.Cells["B5"].Value = "Mã bán hàng";
            worksheet.Cells["C5"].Value = "Số bàn";
            worksheet.Cells["D5"].Value = "Nhân viên bán hàng";
            worksheet.Cells["E5"].Value = "Ngày tạo";
            worksheet.Cells["F5"].Value = "Kiểu hoá đơn";
            worksheet.Cells["G5"].Value = "Mô tả";
            worksheet.Cells["H5"].Value = "Kiểu thanh toán";
            worksheet.Cells["I5"].Value = "Tên khách hàng";
            worksheet.Cells["J5"].Value = "Tên cửa hàng";
            worksheet.Cells["K5"].Value = "Giảm giá theo khuyến mãi";
            worksheet.Cells["L5"].Value = "Giảm trừ";
            worksheet.Cells["M5"].Value = "Tiền khách hàng đưa";
            worksheet.Cells["N5"].Value = "Tiền khách hàng nhận";
            worksheet.Cells["O5"].Value = "Phí vận chuyển";
            worksheet.Cells["P5"].Value = "Tổng tiền";
            worksheet.Cells["Q5"].Value = "Thành tiền";
            worksheet.Cells["A5:Q5"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells["A5:Q5"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
            worksheet.Cells["A5:Q5"].Style.Font.Bold = true;
            row = 6;
            foreach (var item in data.Select((value, i) => new {value, i}))
            {
                var customer = new Customer();
                if (item.value.CustomerID != null)
                    customer = await _context.Customers.FindAsync(item.value.CustomerID);
                worksheet.Cells[row, 1].Value = item.i;
                worksheet.Cells[row, 2].Value = item.value.Code;
                worksheet.Cells[row, 3].Value = item.value.TableNum;
                worksheet.Cells[row, 4].Value = item.value.Cashier;
                worksheet.Cells[row, 5].Value = item.value.CreateDate.ToShortDateString();
                worksheet.Cells[row, 6].Value = item.value.OrderType == OrderType.InPlace ? "Tại cửa hàng" :
                    item.value.OrderType == OrderType.TakeAway ? "Mang về" : "Ship đồ";
                worksheet.Cells[row, 7].Value = item.value.Description;
                worksheet.Cells[row, 8].Value =
                    item.value.PaymentType == PaymentType.Cash ? "Tiền mặt" : "Chuyển khoản";
                worksheet.Cells[row, 9].Value = customer != null ? customer.FullName : "";
                worksheet.Cells[row, 10].Value = item.value.Branch.Name;
                worksheet.Cells[row, 11].Value = item.value.ReducePromotion.ToString("#,###", cul.NumberFormat);
                worksheet.Cells[row, 12].Value = item.value.ReduceAmount.ToString("#,###", cul.NumberFormat);
                worksheet.Cells[row, 13].Value = item.value.CustomerPut.ToString("#,###", cul.NumberFormat);
                worksheet.Cells[row, 14].Value = item.value.CustomerReceive.ToString("#,###", cul.NumberFormat);
                worksheet.Cells[row, 15].Value = item.value.ShippingFee.ToString("#,###", cul.NumberFormat);
                worksheet.Cells[row, 16].Value = item.value.SubAmount.ToString("#,###", cul.NumberFormat);
                worksheet.Cells[row, 17].Value = item.value.TotalAmount.ToString("#,###", cul.NumberFormat);
                row++;
            }

            row++;
            worksheet.Cells[$"A{row}"].Value = "Doanh thu bán hàng";
            using (var r = worksheet.Cells[$"A{row}:P{row}"])
            {
                r.Merge = true;
                r.Style.Font.Size = 14;
                r.Style.Font.Color.SetColor(Color.Black);
                r.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(223, 249, 251));
            }

            worksheet.Cells[$"Q{row}"].Value = (data.Sum(x => x.TotalAmount)).ToString("#,###", cul.NumberFormat);
            using (var r = worksheet.Cells[$"Q{row}:Q{row}"])
            {
                r.Merge = true;
                r.Style.Font.Size = 14;
                r.Style.Font.Color.SetColor(Color.White);
                r.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(116, 185, 255));
            }

            // set some core property values
            xlPackage.Workbook.Properties.Title = "Báo cáo";
            xlPackage.Workbook.Properties.Author = "Quản lý";
            xlPackage.Workbook.Properties.Subject = "Báo cáo";
            // save the new spreadsheet
            xlPackage.Save();
            // Response.Clear();
        }

        stream.Position = 0;
        var fileName = "temp-order.xlsx";
        await _storageService.SaveFileAsync(stream, fileName);
        return _storageService.GetPathBE(fileName);
    }

    public async Task<Respond<PagedList<ProductQuantityVm>>> GetProductInOrder(string? branchId,
        Search search)
    {
        var orders = await _context.Orders
            .Include(x => x.OrderDetails)
            .ThenInclude(x => x.Product)
            .Where(x => x.BranchID == branchId)
            .ToListAsync();
        var orderDetails = new List<OrderDetail>();
        if (orders != null && orders.Count > 0)
        {
            foreach (var item in orders)
            {
                orderDetails.AddRange(item.OrderDetails);
            }
        }

        List<ProductQuantityVm> reponds = new List<ProductQuantityVm>();
        foreach (var item in orderDetails)
        {
            bool isAdd = true;
            foreach (var repond in reponds)
            {
                if (repond.ProductID == item.ProductID)
                {
                    repond.Quantity += item.Quantity;
                    isAdd = false;
                }
            }

            if (isAdd)
            {
                reponds.Add(new ProductQuantityVm()
                {
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    ProductName = item.Product.Name,
                    LinkImage = item.Product.LinkImage,
                    Unit = item.Product.ProductType == ProductType.Drink
                        ? "Đồ uống"
                        : item.Product.ProductType == ProductType.Food
                            ? "Đồ ăn"
                            : item.Product.ProductType == ProductType.InDay
                                ? "Trong ngày"
                                : "Khác"
                });
            }
        }

        int totalRow = reponds.Count();
        if (search.IsPging)
        {
            reponds = reponds
                .Skip((search.PageNumber - 1) * search.PageSize)
                .Take(search.PageSize)
                .ToList();
        }

        // select
        var pagedResult = new PagedList<ProductQuantityVm>()
        {
            TotalRecord = totalRow,
            PageSize = search.PageSize,
            CurrentPage = search.PageNumber,
            TotalPages = (int) Math.Ceiling((double) totalRow / search.PageSize),
            Items = reponds,
        };
        return new Respond<PagedList<ProductQuantityVm>>
        {
            Message = "Thành công",
            Result = 1,
            Data = pagedResult,
        };
    }

    public async Task<Respond<PagedList<ProductQuantityVm>>> GetProductInOrderAllBranch(Search search)
    {
        var orders = await _context.Orders
            .Include(x => x.OrderDetails)
            .ThenInclude(x => x.Product)
            .ToListAsync();
        var orderDetails = new List<OrderDetail>();
        if (orders != null && orders.Count > 0)
        {
            foreach (var item in orders)
            {
                orderDetails.AddRange(item.OrderDetails);
            }
        }

        List<ProductQuantityVm> reponds = new List<ProductQuantityVm>();
        foreach (var item in orderDetails)
        {
            bool isAdd = true;
            foreach (var repond in reponds)
            {
                if (repond.ProductID == item.ProductID)
                {
                    repond.Quantity += item.Quantity;
                    isAdd = false;
                }
            }

            if (isAdd)
            {
                reponds.Add(new ProductQuantityVm()
                {
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    ProductName = item.Product.Name,
                    LinkImage = item.Product.LinkImage,
                    Unit = item.Product.ProductType == ProductType.Drink
                        ? "Đồ uống"
                        : item.Product.ProductType == ProductType.Food
                            ? "Đồ ăn"
                            : item.Product.ProductType == ProductType.InDay
                                ? "Trong ngày"
                                : "Khác"
                });
            }
        }

        int totalRow = reponds.Count();
        if (search.IsPging)
        {
            reponds = reponds
                .Skip((search.PageNumber - 1) * search.PageSize)
                .Take(search.PageSize)
                .ToList();
        }

        // select
        var pagedResult = new PagedList<ProductQuantityVm>()
        {
            TotalRecord = totalRow,
            PageSize = search.PageSize,
            CurrentPage = search.PageNumber,
            TotalPages = (int) Math.Ceiling((double) totalRow / search.PageSize),
            Items = reponds,
        };
        return new Respond<PagedList<ProductQuantityVm>>
        {
            Message = "Thành công",
            Result = 1,
            Data = pagedResult,
        };
    }

    public async Task<Respond<Order>> GetById(string orderId)
    {
        var order = await _context.Orders
            .Include(x => x.OrderDetails)
            .ThenInclude(x => x.OrderToppingDetails)
            .Include(x => x.OrderDetails)
            .ThenInclude(x => x.Product)
            .Include(x => x.OrderDetails)
            .ThenInclude(x => x.Size)
            .FirstOrDefaultAsync(x => x.ID == orderId);
        if (order == null)
            return new Respond<Order>
            {
                Message = "Không tồn tại hoá đơn",
                Result = 0,
                Data = null,
            };
        return new Respond<Order>
        {
            Message = "Thành công",
            Result = 1,
            Data = order,
        };
    }

    public async Task<MessageResult> UpdateStatus(string orderId, OrderStatus status)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(x => x.ID == orderId);
        if (order == null)
            return new MessageResult
            {
                Message = "Không tồn tại hoá đơn",
                Result = 0,
            };
        order.Status = status;
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
        return new MessageResult
        {
            Message = "Thành công",
            Result = 1,
        };
    }

    public async Task<MessageResult> Delete(string orderId)
    {
        var order = await _context.Orders
            .Include(x => x.OrderDetails)
            .ThenInclude(x => x.OrderToppingDetails)
            .FirstOrDefaultAsync(x => x.ID == orderId);
        if (order == null)
            return new MessageResult
            {
                Message = "Không tồn tại hoá đơn",
                Result = 0,
            };
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return new MessageResult
        {
            Message = "Thành công",
            Result = 1,
        };
    }

    public void Dispose()
    {
        GC.Collect(2, GCCollectionMode.Forced, true);
        GC.WaitForPendingFinalizers();
        GC.SuppressFinalize(this);
    }
}