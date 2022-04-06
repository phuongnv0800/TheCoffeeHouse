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
          @page { size: auto; margin: 2mm 2mm 2mm 2mm; }
          #invoice { padding: 0; zoom: <xsl:value-of select="Scale"/>%; font-family: 'Arial', sans-serif; font-size: 30px }
          .table-invoice { border-collapse: collapse; width: 100%}
          .table-invoice tr th,table tr td { padding: 3px; border: 2px solid black; font-size: 30px  }
          .table-invoice tr { text-align: center;}
          #FoodName, #Unit { text-align: left }
          #Price, #Amount { text-align: right }
          .text-center { text-align: center; }
          .text-left { text-align: left; }
          .text-right { text-align: right; }
          .container { width: 1024px; margin: 0 auto; padding: 20px;}
          #info { margin-bottom: 10px; }
          #info table tr td { border: none; text-align: left;}
		  #info p, #header p { margin: 3px 0}
          #InvoiceName { margin-bottom: 7px }
          .tb-small { width: 400px; }
          .tb-small-10 { width: 200px; }
          .tb-big { width: 400px }
          #last-row td, #payment tr td { border: none; }
          #payment { margin-top: 10px; }
          background-size: cover; width: 50%; margin: 0 auto; }
          #footer_4 { margin-top: 10px; font-style: italic }
          #InvoiceName { margin-top: 0;}
          .pt-15 { padding-top: 15px;}
          .pt-5 { padding-top: 5px;}
          #footer_5 { font-size: 30px;}
          .bold { font-weight: bold}
          .title { font-size: 50px }
          #Resname{ font-size: 60px }
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
          <div id="header" class="text-center">
            <div>
              <p id="Resname">
                <b>
                  <xsl:value-of select="Restaurant/Name"/>
                </b>
              </p>
              <p style="font-size: 45px">
                <xsl:value-of select="Restaurant/Address"/>
              </p>
              <p style="font-size: 45px">
                Hotline: <xsl:value-of select="Restaurant/Phone"/>
              </p>
              <p>
                <xsl:value-of select="Restaurant/Headercontent"/>
              </p>
            </div>
            <div class="box-b" style="width: 40%; margin: 0 auto">
              <span></span>
            </div>
            <div>
              <p style="font-size: 65px">
                <b>
                  <xsl:value-of select="Title"/>
                </b>
              </p>
            </div>
          </div>
          <div id="content">
            <div id="info" style="margin-bottom: 10px">
              <p style="font-size: 45px">
                <b>Bàn:</b>
                <xsl:value-of select="DinnerTable"/>
              </p>
              <p style="font-size: 45px">
                <b>Ngày:</b>
                <xsl:value-of select="CreateDate"/>
              </p>
            </div>
            <div class="box-b">
              <span></span>
            </div>
            <div style="margin: 20px 0" >
              <table class="table-invoice">
                <tr>
                  <td class="text-left bold" style="font-size: 50px">TÊN MÓN</td>
                  <td class="bold" style="font-size: 50px; text-align: center">SL</td>
                  <td class="bold" style="font-size: 50px; text-align: center">ĐV</td>
                  <td class="bold" style="font-size: 50px; text-align: center">ĐG</td>
                  <td class="bold" style="font-size: 50px; text-align: center">TT</td>
                </tr>

                <xsl:for-each select="Detail/InvoiceDetail">
                  <tr>
                    <td id="FoodName" style="font-size: 50px">
                      <xsl:variable name="i" select="position()" />
                      <xsl:value-of select="$i"/>
                      .<xsl:value-of select="FoodName"/>
                    </td>
                    <td id="Quantity" style="font-size: 50px">
                      <xsl:value-of select="Quantity"/>
                    </td>
                    <td id="Unit" style="font-size: 50px">
                      <xsl:value-of select="Unit"/>
                    </td>
                    <td id="Price" style="font-size: 50px">
                      <xsl:value-of select="translate(translate(translate(format-number(translate(Price,',','.'), '###,##0.##'),',','?'),'.',','),'?','.')"/>
                    </td>
                    <td id="Amount" style="font-size: 50px">
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
                <tr>
                  <td class="text-left bold" colspan="4" style="font-size: 50px">THANH TOÁN</td>
                  <td class="text-right money bold" style="font-size: 50px">
                    <xsl:value-of select="translate(translate(translate(format-number(translate(Payment,',','.'), '###,##0.##'),',','?'),'.',','),'?','.')"/> đ
                  </td>
                </tr>
				  <tr>
					  <td class="text-left bold" colspan="4" style="font-size: 50px">KHUYẾN MÃI VPP</td>
					  <td class="text-right money bold" style="font-size: 50px">
						  <xsl:value-of select="translate(translate(translate(format-number(translate(MoneyConvertPoint,',','.'), '###,##0.##'),',','?'),'.',','),'?','.')"/> đ
					  </td>
				  </tr>
                <tr>
                  <td class="text-left" colspan="4" style="font-size: 50px">KHÁCH ĐƯA</td>
                  <td class="money text-right" style="font-size: 50px">
                    <xsl:value-of select="translate(translate(translate(format-number(translate(GuestPut,',','.'), '###,##0.##'),',','?'),'.',','),'?','.')"/> đ
                  </td>
                </tr>
                <tr>
                  <td class="text-left bold" colspan="4" style="font-size: 50px">TRẢ KHÁCH</td>
                  <td class="money text-right bold" style="font-size: 50px">
                    <xsl:value-of select="translate(translate(translate(format-number(translate(GuestReceive,',','.'), '###,##0.##'),',','?'),'.',','),'?','.')"/> đ
                  </td>
                </tr>
              </table>
            </div>
          </div>
          <div id="footer">
            <div id="footer_4" class="text-center" style="font-size: 40px">
              <xsl:for-each select="Footercontent/Line">
                <p>
                  <xsl:value-of select="Value" />
                </p>
              </xsl:for-each>
            </div>
          </div>
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
