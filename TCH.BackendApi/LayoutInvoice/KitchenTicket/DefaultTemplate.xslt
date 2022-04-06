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
          @page { size: auto; margin: 2mm 2mm 2mm 2mm; }
          #invoice { padding: 0; font-family: 'Arial', sans-serif; font-size: 14px }
          .table-invoice { border-collapse: collapse; width: 100%}
          .table-invoice tr th,table tr td { padding: 3px; border: 0px solid black; font-size: 10px  }
          .table-invoice tr { text-align: center;}
          #FoodName, #Unit { text-align: left }
          #Price, #Amount { text-align: right }
          .text-center { text-align: center; }
          .text-left { text-align: left; }
          .text-right { text-align: right; }
          .container { width: 500; margin: 0 auto; padding: 10px;}
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
          .pt-15 { padding-top: 10px;}
          .pt-5 { padding-top: 5px;}
          #footer_5 { font-size: 10px;}
          .bold { font-weight: bold}
          .title { font-size: 10px }
          #Resname{ font-size: 10px }
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
              <p class="bold" style="font-size: 30px; text-align: center; margin-top: 10px">
                <xsl:value-of select="Restaurant/DinnerTableName"/>
              </p>
              <p style="font-size: 25px">
                <xsl:value-of select="Restaurant/OrderCode"/>
              </p>
              <p style="font-size: 25px">
                <xsl:value-of select="Restaurant/UpdateDate"/>
              </p>
              <p style="font-size: 25px">
                <xsl:value-of select="Restaurant/UserName"/>
              </p>
            </div>
          </div>

          <div id="header">
            <div>
              <p class="bold" style="font-size: 25px; text-align: center; margin-top: 10px">
                <xsl:value-of select="Title"/>
              </p>
            </div>
          </div>

          <div id="content">
            <div id="info" style="margin-bottom: 10px">

              <div style="text-align: center; font-size: 20px;">
                <span>---------------------------------</span>
              </div>


              <div style="margin: 10px 0" >

                <xsl:for-each select="Detail/OldKitchenOrderDetail">

                  <p style="font-size: 25px; margin-top: 10px">
                    <xsl:value-of select="Quantity"/> x (<xsl:value-of select="Unit"/>) <xsl:value-of select="FoodName"/>
                  </p>

                </xsl:for-each>

                <xsl:for-each select="Detail/KitchenOrderDetail">

                  <p class="bold" style="font-size: 25px; margin-top: 10px">
                    <xsl:value-of select="Quantity"/> x (<xsl:value-of select="Unit"/>) <xsl:value-of select="FoodName"/>
                  </p>

                </xsl:for-each>

                <xsl:for-each select="Detail/DeletedKitchenOrderDetail">

                  <p style="font-size: 25px; margin-top: 10px">
                    <del>
                      <xsl:value-of select="Quantity"/> x (<xsl:value-of select="Unit"/>) <xsl:value-of select="FoodName"/>
                    </del>
                  </p>

                </xsl:for-each>
              </div>

              <div style="text-align: center; font-size: 20px;">
                <span>---------------------------------</span>
              </div>

            </div>
          </div>
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
