<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:fo="http://www.w3.org/1999/XSL/Format" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:variable name="itemCount">
    <xsl:value-of select="count(Invoice//Detail//InvoiceDetail)" />
  </xsl:variable>
  <xsl:output omit-xml-declaration="yes" indent="yes" encoding="UTF-8" />  
  <xsl:template match="/*">
    <html>
      <head>
         <xsl:element name="link">
          <xsl:attribute name="href">https://fonts.googleapis.com/css2?family=Miriam+Libre:wght@400;700</xsl:attribute>
          <xsl:attribute name="rel">stylesheet</xsl:attribute>
          <xsl:attribute name="type">text/css</xsl:attribute>
        </xsl:element>
        
        <meta http-equiv="Content-Type" content="text/html;charset=utf-8"/>
        <title>
         <xsl:value-of select="Title"/>
        </title>
        <script src="http://html2canvas.hertzen.com/dist/html2canvas.min.js">(function(){})()</script>
        <style type="text/css">
          body { font-family: 'Times New Roman', sans-serif; font-size: 20px }
          table tr td{font-size: 20px}
          @page { size: auto; margin: 2mm 2mm 2mm 2mm; }
          #invoice { padding: 0; zoom: 50%;}
          .table-invoice { border-collapse: collapse; width: 100%}
          .table-invoice tr th,table tr td { padding: 3px; border: 0px solid black; }
          .table-invoice tr { text-align: center;}
          #FoodName, #Unit { text-align: left }
          #Price, #Amount { text-align: right }
          .text-center { text-align: center; }
          .text-left { text-align: left; }
          .text-right { text-align: right; }
          .container { width: 500px; margin: 0 auto; padding: 20px;}
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
              <div style="display: inline-block; width: 30%; vertical-align: top">
                <img>
                  <xsl:attribute name="width">100%</xsl:attribute>
                  <xsl:attribute name="height">150px</xsl:attribute>
                  <xsl:attribute name="src">
                    <xsl:value-of select="Restaurant/Logo"/>
                  </xsl:attribute>
                </img>
              </div>
              <div style="display: inline-block;  width: 68%; vertical-align: top">
                <p id="Resname"> <xsl:value-of select="Restaurant/Name"/> </p>
                <p> <xsl:value-of select="Restaurant/Address"/> </p>
                <p> <xsl:value-of select="Restaurant/Phone"/> </p>
                <p> <xsl:value-of select="Restaurant/Headercontent"/> </p>
              </div>          
            </div>
            <div>
               <p style="font-size: 1.5em; text-align: center"> <xsl:value-of select="Title"/> </p>           
            </div>
          </div>
          <div id="content">
            <div id="info" style="margin-bottom: 20px">
              <p class="text-center"> <xsl:value-of select="DinnerTable"/> </p>
              <p class="text-center"> <xsl:value-of select="InvoiceCode"/> </p>
              <p class="text-center"> <xsl:value-of select="UserCreate"/> </p>
              <p class="text-center"> <xsl:value-of select="CreateDate"/> </p>
            </div>
            <div class="box-b">
              <span></span>
            </div>
            <div style="margin: 20px 0" >
              <table class="table-invoice">
                <tr>
                  <td class="text-left">TÊN MÓN</td>
                  <td>SỐ LƯỢNG</td>
                  <td class="text-right">ĐƠN GIÁ</td>
                  <td class="text-right">THÀNH TIỀN</td>
                </tr>
               
                <xsl:for-each select="Detail/InvoiceDetail">
                  <tr>
                    <td id="FoodName">
                      <xsl:variable name="i" select="position()" />
                      <xsl:value-of select="$i"/>
                      .<xsl:value-of select="FoodName"/>
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
            <div class="box-b">
              <span></span>
            </div>
            <div id="payment" style="margin-top: 20px">               
              <table class="table-invoice">
                <tr id="last-row">
                  <td class="text-left" colspan="4">THÀNH TIỀN</td>
                  <td class="text-right money">
                    <xsl:value-of select="translate(translate(translate(format-number(translate(SumAmount,',','.'), '###,##0.##'),',','?'),'.',','),'?','.')"/> đ
                  </td>
                </tr>
                <tr id="last-row">
                  <td class="text-left" colspan="4">KHUYẾN MẠI</td>
                  <td class="text-right money" >
                    <xsl:value-of select="translate(translate(translate(format-number(translate(ReduceAmount,',','.'), '###,##0.##'),',','?'),'.',','),'?','.')"/>  đ
                  </td>
                </tr>
                <tr>
                  <td class="text-left" colspan="4">THANH TOÁN</td>
                  <td class="text-right money">
                    <xsl:value-of select="translate(translate(translate(format-number(translate(Payment,',','.'), '###,##0.##'),',','?'),'.',','),'?','.')"/> đ
                  </td>
                </tr>
              <tr>
                  <td class="text-left" colspan="4">KHÁCH ĐƯA</td>
                  <td class="money text-right">
                    <xsl:value-of select="translate(translate(translate(format-number(translate(GuestPut,',','.'), '###,##0.##'),',','?'),'.',','),'?','.')"/> đ
                  </td>
                </tr>
                <tr>
                  <td class="text-left" colspan="4">TRẢ KHÁCH</td>
                  <td class="money text-right">
                    <xsl:value-of select="translate(translate(translate(format-number(translate(GuestReceive,',','.'), '###,##0.##'),',','?'),'.',','),'?','.')"/> đ
                  </td>
                </tr>
              </table>
            </div>
          </div>
           <div class="box-b">
              <span></span>
            </div>
          <div id="footer">
            <div id="footer_4" class="text-center">
              <xsl:for-each select="Footercontent/Line">
                <p><xsl:value-of select="Value" /></p>
              </xsl:for-each>
            </div>
             <div class="box-b">
              <span></span>
            </div>
            <div id="footer_5" class="text-center pt-5">
              VNPT - eRestaurant - Hệ thống quản lý nhà hàng
            </div>
          </div>
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
