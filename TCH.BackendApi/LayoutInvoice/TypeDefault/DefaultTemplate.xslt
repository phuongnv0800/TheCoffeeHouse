<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:fo="http://www.w3.org/1999/XSL/Format" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:variable name="itemCount">
    <xsl:value-of select="count(Invoice//Detail//InvoiceDetail)" />
  </xsl:variable>
  <xsl:output omit-xml-declaration="yes" indent="yes" encoding="UTF-8" />  
  <xsl:template match="/*">
    <html>
      <head>      
        <meta http-equiv="Content-Type" content="text/html;charset=utf-8"/>
        <title>
         <xsl:value-of select="Title"/>
        </title>
        <script src="http://html2canvas.hertzen.com/dist/html2canvas.min.js">(function(){})()</script>
        <style type="text/css">
          body { font-family: 'Arial', sans-serif; font-size: 20px }
          table tr td{font-size: 20px}
          @page { size: auto; margin: 2mm 2mm 2mm 2mm; }
          #invoice { padding: 0; zoom: 50%;}
          .table-invoice { border-collapse: collapse; width: 100%}
          .table-invoice tr th, tr td { padding: 5px; border: 1px solid black; }
          .table-invoice tr { text-align: center;}
          .table-invoice tr td { border-bottom: 0px; border-top: 0px}
          .table-invoice { border-bottom: 1px solid black;}
          #FoodName, #Unit { text-align: left }
          #Price, #Amount { text-align: right }
          .text-center { text-align: center; }
          .text-left { text-align: left; }
          .text-right { text-align: right; }
          .container { width: 1000px; margin: 0 auto; padding: 20px;}
          #info { margin-bottom: 10px; }
          #info table tr td { border: none; text-align: left;}
          #InvoiceName { margin-bottom: 7px }
          .tb-small { width: 11% }
          .tb-big { width: 40% }
          #last-row td, #payment tr td { border: none; }
          #payment { margin-top: 10px; }
          background-size: cover; width: 50%; margin: 0 auto; }
          #footer_4 { margin-top: 10px; font-style: italic }
          #InvoiceName { margin-top: 0;}
          .pt-15 { padding-top: 15px;}
          .pt-5 { padding-top: 5px;}
          #footer_5 { font-size: 12px;}
          .bold { font-weight: bold}
          .title { font-size: 1.5em }
          #Resname{ font-size: 1.5em}
          body p{margin: 10 0}
          .box-b {
          height: 1px;
          border-bottom: 1px dashed black;
          }
          .box-ranger{ height: 6px }
        </style>
      </head>
      <body>
        <div class="container" id="invoice">
          <div id="header">
            <div>
              <div style="display: inline-block; width: 30%; vertical-align: top; text-align: left">
                <p>
                  <xsl:value-of select="InvoiceCode"/>
                </p>
                <p>
                  <xsl:value-of select="CreateDate"/>
                </p>
              </div>
              <div style="display: inline-block;  width: 63%; vertical-align: top; float: right; text-align: right">
                <p id="Resname"> <xsl:value-of select="Restaurant/Name"/> </p>
                <p>
                  <xsl:value-of select="Restaurant/Phone"/>
                </p>
                <p> <xsl:value-of select="Restaurant/Address"/> </p>
                <p> <xsl:value-of select="Restaurant/Headercontent"/> </p>
              </div>          
            </div>
            <div style="clear: both">
               <p class="bold" style="font-size: 1.5em; text-align: center; margin-top: 20px"> <xsl:value-of select="Title"/> </p>           
            </div>
          </div>
          <div id="content">
            <div id="info" style="margin-bottom: 20px">
              <p class="text-center">
                Thu ngân  <xsl:value-of select="UserCreate"/> 
              </p>
              <p class="text-center">Bàn/Phòng: <xsl:value-of select="DinnerTable"/> </p>
            </div>
            <div style="margin: 20px 0" >
              <table class="table-invoice">
                <tr>
                  <th class="text-center bold" style="width: 50px">STT</th>
                  <th class="text-left bold">TÊN MÓN</th>
                  <th class="bold" style="width: 80px">SỐ LƯỢNG</th>
                  <th class="text-right bold">ĐƠN GIÁ</th>
                  <th class="text-right bold">THÀNH TIỀN</th>
                </tr>
               
                <xsl:for-each select="Detail/InvoiceDetail">
                  <tr>
                    <td class="text-center" id="STT">
                      <xsl:variable name="i" select="position()" />
                      <xsl:value-of select="$i"/>
                    </td>
                    <td id="FoodName">
                      <xsl:value-of select="FoodName"/>
                    </td>
                    <td id="Quantity">
                      <xsl:value-of select="Quantity"/>
                    </td>
                    <td id="Price">
                      <xsl:value-of select="translate(translate(translate(format-number(translate(Price,',','.'), '###,##0.##'),',','?'),'.',','),'?','.')"/>
                    </td>
                    <td id="Amount">
                      <xsl:value-of select="translate(translate(translate(format-number(translate(Amount,',','.'), '###,##0.##'),',','?'),'.',','),'?','.')"/>
                    </td>
                  </tr>

                </xsl:for-each>
                
              </table>
            </div>
            <div id="payment" style="margin-top: 20px">               
              <table class="table-invoice">
                <tr id="last-row">
                  <td class="text-right bold" colspan="3">TRỊ GIÁ (VNĐ)</td>
                  <td class="text-right money bold">
                    <xsl:value-of select="translate(translate(translate(format-number(translate(SumAmount,',','.'), '###,##0.##'),',','?'),'.',','),'?','.')"/> đ
                  </td>
                </tr>
                <tr id="last-row">
                  <td class="text-right" colspan="3">CHIẾT KHẤU (VNĐ)</td>
                  <td class="text-right money" >
                    <xsl:value-of select="translate(translate(translate(format-number(translate(ReduceAmount,',','.'), '###,##0.##'),',','?'),'.',','),'?','.')"/>  đ
                  </td>
                </tr>
                <tr>
                  <td class="text-right bold" colspan="3">THANH TOÁN (VNĐ)</td>
                  <td class="text-right money bold">
                    <xsl:value-of select="translate(translate(translate(format-number(translate(Payment,',','.'), '###,##0.##'),',','?'),'.',','),'?','.')"/> đ
                  </td>
                </tr>
              </table>
            </div>
          </div>
          <div id="footer">
            <div id="footer_4" class="text-center">
              <xsl:for-each select="Footercontent/Line">
                <p><xsl:value-of select="Value" /></p>
              </xsl:for-each>
            </div>
          </div>
          <div id="footer-sign" style="margin-top: 20px">
            <div style="display: inline-block; width: 49%; float: left; text-align: center">
              NGƯỜI BÁN HÀNG
            </div>
            <div style="display: inline-block; width: 49%; float: RIGHT; text-align: center">
              KẾ TOÁN
            </div>
          </div>
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
