<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:fo="http://www.w3.org/1999/XSL/Format"
    xmlns:date="http://exslt.org/dates-and-times"
                extension-element-prefixes="date">
    <xsl:template match="GDK">
        <fo:root>
            <fo:layout-master-set>
                <fo:simple-page-master master-name="A4" page-height="29.7cm" page-width="21cm" margin-top="2cm"
                               margin-bottom="2cm" margin-left="2cm" margin-right="2cm"
                               >

                    <fo:region-body margin-bottom="5mm" margin-top="5mm"  />
                    <fo:region-before region-name="header"  extent="5mm"/>
                    <fo:region-after region-name="footer" extent="5mm"/>

                </fo:simple-page-master>
            </fo:layout-master-set>
            <xsl:for-each select="./GDK_ROW">
                <fo:page-sequence master-reference="A4">
                    
                    <fo:static-content flow-name="footer" font-size="7pt">
                        <fo:block>Footer</fo:block>
                           <fo:block text-align="center">
                                Page <fo:page-number/> / <fo:page-number-citation ref-id="lastBlock"/>
                            </fo:block>
                    </fo:static-content>

                    <fo:static-content flow-name="header" font-size="7pt">
                        <fo:block>Header</fo:block>
                    </fo:static-content>                    

                    <fo:flow flow-name="xsl-region-body" font-size="8pt">
                        <fo:block>
                            <xsl:value-of select="contract_nr"/>
                        </fo:block>
                        <xsl:if test="position() = last()">
                            <fo:block id="lastBlock"/>
                        </xsl:if>
                    </fo:flow>

                 </fo:page-sequence>
            </xsl:for-each>
        </fo:root>
    </xsl:template>
</xsl:stylesheet>