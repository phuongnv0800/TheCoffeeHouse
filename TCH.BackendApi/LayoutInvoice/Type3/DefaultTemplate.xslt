<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:fo="http://www.w3.org/1999/XSL/Format" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:variable name="itemCount">
    <xsl:value-of select="count(Invoice//Detail//InvoiceDetail)" />
  </xsl:variable>
  
  <xsl:variable name="x">
	  <xsl:for-each select="Invoice/Detail/InvoiceDetail">
		<xsl:element name="SumQuantity">
		 <xsl:value-of select="Quantity * DecimalFactor"/>
		</xsl:element>
	  </xsl:for-each>
  </xsl:variable>
  
  <xsl:output omit-xml-declaration="yes" indent="yes" encoding="UTF-8" />
  <xsl:template match="/*">
    <html>
      <head>	  
        <xsl:element name="link">
		  <xsl:attribute name="href">https://fonts.googleapis.com/css2?family=Poppins:wght@400;600</xsl:attribute>
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
          #invoice-css { padding: 0; zoom: <xsl:value-of select="Scale"/>%; font-family: 'Poppins', sans-serif; font-size: 30px }
          .table-invoice { border-collapse: collapse; width: 100%;}
          .table-invoice tr th,table tr td { padding: 3px; font-size: 30px  }
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
          #InvoiceName { margin-top: 0;}
          .pt-15 { padding-top: 15px;}
          .pt-5 { padding-top: 5px;}
          .bold { font-weight: bold}
          .title { font-size: 50px }
          body p{margin: 10 0}
          .box-b { height: 2px; border-bottom: 2px dashed black; }
          .box-ranger{ height: 6px }
		  #logo {margin: 10px 0}
		  #footer_4 { margin-top: 20px}
        </style>
      </head>
      <body>
        <div class="container" id="invoice-css">
           <div id="header" class="text-center">
            <div>
              <p id="Resname" style="font-size: 60px">
                <b>
                  <xsl:value-of select="Restaurant/Name"/>
                </b>
              </p>
              <p style="font-size: 40px">
                <xsl:value-of select="Restaurant/Address"/>
              </p>
              <p style="font-size: 40px">
                Hotline: <xsl:value-of select="Restaurant/Phone"/>
              </p>
              <p style="font-size: 40px">
                <xsl:value-of select="Restaurant/Headercontent"/>
              </p>
            </div>
            <div style="margin-top: 60px">
              <p style="font-size: 55px; text-align: center">
                <b>
                  <xsl:value-of select="Title"/>
                </b>
              </p>
			  <p class="text-center" style="font-size: 40px">
                <xsl:value-of select="InvoiceCode"/>
              </p>
			   <p class="text-center" style="font-size: 40px">
                <xsl:value-of select="CreateDate"/>
              </p>
			  <p class="text-center" style="font-size: 40px">
                <xsl:value-of select="DinnerTable"/>
              </p>
			   <p class="text-center" style="font-size: 40px">
                <xsl:value-of select="UserCreate"/>
              </p>
            </div>
          </div>
          <div id="content">
            <div style="margin: 40px 0 0 0" >
              <table class="table-invoice">
                <tr>
                  <td class="bold" style="font-size: 40px; border-bottom: 1px solid black; text-align: left; width: 40%">T??n m??n</td>
				  <td class="bold" style="font-size: 40px; border-bottom: 1px solid black; text-align: right;">SL</td>
				  <td class="bold" style="font-size: 40px; border-bottom: 1px solid black; text-align: center;">????n gi??</td>
                  <td class="bold" style="font-size: 40px; border-bottom: 1px solid black; text-align: right;">Th??nh ti???n</td>
                </tr>
				<xsl:for-each select="Detail/InvoiceDetail">	
					<xsl:choose>
						<xsl:when test="DecimalFactor &gt; 1">
							<tr>
								<td id="FoodName" style="font-size: 40px; text-align: left; width: 40%">
									<xsl:variable name="i" select="position()" />
								    <xsl:value-of select="$i"/>
								    .<xsl:value-of select="FoodName"/>
								    (<xsl:value-of select="Unit"/>) 	
								</td>
								<td id="Quantity" style="font-size: 40px; text-align: right; vertical-align: bottom;">
									<xsl:value-of select="DecimalFactor * Quantity"/> x
								</td>
								<td id="Price" style="font-size: 40px; text-align: center; vertical-align: bottom;">
									<xsl:value-of select="translate(translate(translate(format-number(translate(Price,',','.'), '###,##0.##'),',','?'),'.',','),'?','.')"/>
								</td>
								<td id="Amount" style="font-size: 40px; text-align: right; vertical-align: bottom;">
								  <xsl:value-of select="translate(translate(translate(format-number(translate(Amount,',','.'), '###,##0.##'),',','?'),'.',','),'?','.')"/>
								</td>
							</tr>
						</xsl:when>
						<xsl:otherwise>
							<tr>
								<td id="FoodName" style="font-size: 40px; text-align: left; width: 40%">
									<xsl:variable name="i" select="position()" />
								    <xsl:value-of select="$i"/>
								    .<xsl:value-of select="FoodName"/>
								    (<xsl:value-of select="Unit"/>) 	
								</td>
								<td id="Quantity" style="font-size: 40px; text-align: right; vertical-align: bottom;">
								    <xsl:value-of select="DecimalFactor * Quantity"/> x
								</td>
								<td id="Price" style="font-size: 40px; text-align: center; vertical-align: bottom;">
									<xsl:value-of select="translate(translate(translate(format-number(translate(Price,',','.'), '###,##0.##'),',','?'),'.',','),'?','.')"/>
								</td>
								<td id="Amount" style="font-size: 40px; text-align: right; vertical-align: bottom;">
								  <xsl:value-of select="translate(translate(translate(format-number(translate(Amount,',','.'), '###,##0.##'),',','?'),'.',','),'?','.')"/>
								</td>
							</tr>					
						</xsl:otherwise>
					</xsl:choose>
                </xsl:for-each>
				<tr>
					<td class="text-left money bold" style="font-size: 40px; border-top: 1px solid black;">S??? l?????ng v?? ti???n</td>
					<td style="border-top: 1px solid black; font-size: 40px; text-align: right">
						<xsl:value-of select="sum(msxsl:node-set($x)/SumQuantity)"/> 
					</td>
					<td style="border-top: 1px solid black;"></td>
					<td class="text-right money bold" style="font-size: 40px; border-top: 1px solid black;">
						<xsl:value-of select="translate(translate(translate(format-number(translate(SumAmount,',','.'), '###,##0.##'),',','?'),'.',','),'?','.')"/> ??
					</td>
				</tr>
              </table>
            </div>
            <div id="payment">
              <table class="table-invoice">
				  <xsl:if test="SurchargeMoney &gt; 0">
					  <tr>
						  <td class="text-right" style="font-size: 40px;">Ph??? thu</td>
						  <td class="text-right money" style="font-size: 40px">
							  <xsl:value-of select="SurchargeMoney"/>  %
						  </td>
					  </tr>
				  </xsl:if>
				  <xsl:if test="ShippingFee &gt; 0">
					  <tr>
						  <td class="text-right" style="font-size: 40px;">Ph?? ship</td>
						  <td class="text-right money" style="font-size: 40px">
							 <xsl:value-of select="translate(translate(translate(format-number(translate(ShippingFee,',','.'), '###,##0.##'),',','?'),'.',','),'?','.')"/>  ??
						  </td>
					  </tr>
				  </xsl:if>
				  <xsl:if test="MoneyConvertPoint &gt; 0">
					  <tr>
						  <td class="text-right" style="font-size: 40px">S??? ti???n gi???m VPP</td>
						  <td class="text-right money" style="font-size: 40px">
							  - <xsl:value-of select="translate(translate(translate(format-number(translate(MoneyConvertPoint,',','.'), '###,##0.##'),',','?'),'.',','),'?','.')"/>  ??
						  </td>
					  </tr>
				  </xsl:if>
				  <xsl:if test="ReduceAmount &gt; 0">
					  <tr>
						  <td class="text-right" style="font-size: 40px;">Gi???m gi??</td>
						  <td class="text-right money" style="font-size: 40px">
							  - <xsl:value-of select="translate(translate(translate(format-number(translate(ReduceAmount,',','.'), '###,##0.##'),',','?'),'.',','),'?','.')"/>  ??
						  </td>
					  </tr>
				  </xsl:if>
				  <xsl:if test="ReducePromotion &gt; 0">
					  <tr>
						  <td class="text-right" style="font-size: 40px">Khuy???n m???i</td>
						  <td class="text-right money" style="font-size: 40px">
							  - <xsl:value-of select="translate(translate(translate(format-number(translate(ReducePromotion,',','.'), '###,##0.##'),',','?'),'.',','),'?','.')"/>  ??
						  </td>
					  </tr>
				  </xsl:if>
				  <xsl:if test="VAT &gt; 0">
					  <tr>
						  <td class="text-right" style="font-size: 40px;">VAT</td>
						  <td class="text-right money" style="font-size: 40px; border-bottom: 1px solid black">
							 - <xsl:value-of select="VAT"/>  %
						  </td>
					  </tr>
				  </xsl:if>
                <tr>
                  <td class="text-right bold" style="font-size: 40px; border-top: 1px solid black">Thanh to??n</td>
                  <td class="text-right money bold" style="font-size: 40px; border-top: 1px solid black">
                    <xsl:value-of select="translate(translate(translate(format-number(translate(Payment,',','.'), '###,##0.##'),',','?'),'.',','),'?','.')"/> ??
                  </td>
                </tr>
                <tr>
                  <td class="text-right" style="font-size: 40px">Kh??ch ????a</td>
                  <td class="money text-right" style="font-size: 40px">
                    <xsl:value-of select="translate(translate(translate(format-number(translate(GuestPut,',','.'), '###,##0.##'),',','?'),'.',','),'?','.')"/> ??
                  </td>
                </tr>
                <tr>
                  <td class="text-right bold" style="font-size: 40px">Tr??? kh??ch</td>
                  <td class="money text-right bold" style="font-size: 40px">
                    <xsl:value-of select="translate(translate(translate(format-number(translate(GuestReceive,',','.'), '###,##0.##'),',','?'),'.',','),'?','.')"/> ??
                  </td>
                </tr>
              </table>
            </div>
          </div>
          <div id="footer" style="margin-top: 10px">
            <div id="footer_4" class="text-center">
              <xsl:for-each select="Footercontent/Line">
                <p style="font-size: 40px">
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
