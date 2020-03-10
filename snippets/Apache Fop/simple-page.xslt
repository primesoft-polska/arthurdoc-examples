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
                        <fo:block space-after="1cm">
                            <xsl:value-of select="contract_nr"/>
                        </fo:block>
                        <fo:block text-align="justify" space-after="0.5cm">
                            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec non imperdiet lacus. Suspendisse malesuada augue ut consequat posuere. Ut lorem nulla, pellentesque at neque ut, ornare scelerisque tortor. Integer vehicula elit vel nisl molestie, eu finibus purus vestibulum. Suspendisse tellus risus, venenatis et enim non, fringilla suscipit erat. Sed interdum dapibus nisl. Praesent dignissim erat metus, et elementum risus vehicula vitae. Curabitur congue, ligula in venenatis pulvinar, arcu purus mattis augue, quis auctor urna libero sed enim. Nam eu luctus odio, vel scelerisque velit. Mauris lobortis congue diam, nec ultrices ligula hendrerit quis. Nunc tincidunt facilisis ornare. Aenean dapibus suscipit dolor et euismod. Aenean non nisl in lacus volutpat placerat quis et mauris. Vivamus eget sapien sollicitudin, molestie nisl vitae, dictum metus. Aenean ac porttitor quam, id fermentum nisi. Nunc pulvinar mauris vitae diam faucibus, at accumsan nulla luctus.
                        </fo:block>
                        <fo:block text-align="justify" space-after="0.5cm">
                            Aliquam sed maximus velit. Praesent nec risus et lectus blandit tempus non vitae ligula. Fusce maximus, arcu at porttitor faucibus, lacus arcu congue magna, quis lacinia est diam nec velit. Sed semper blandit imperdiet. Cras sit amet tellus nisi. Nulla ac aliquam sapien. Cras interdum ornare nisi. Nunc vitae interdum mi, ac rhoncus lectus. Maecenas ut ante scelerisque, lobortis ipsum eget, blandit diam. Etiam pulvinar diam ligula, quis dignissim sem porttitor sollicitudin. Suspendisse vitae ipsum et arcu placerat tincidunt id id magna. Donec pellentesque lacinia sapien, eu auctor sapien sodales nec. Nullam commodo nunc vitae justo laoreet euismod. Phasellus risus dui, finibus quis convallis a, elementum vel odio. Ut id sapien quam.
                        </fo:block>                            
                            Sed lorem metus, mollis in fermentum eget, aliquet ac dolor. Nam porttitor tellus in mi molestie gravida. Aliquam varius rhoncus est eget blandit. Aenean lobortis sagittis diam, at convallis mauris fermentum quis. Nunc tristique vel velit convallis eleifend. Nulla non eros scelerisque massa auctor volutpat eget et eros. Aliquam id neque nunc. Sed in condimentum tellus, sed luctus nunc. Pellentesque eleifend ante sit amet lacus aliquet, vitae tristique tellus pretium. Integer id diam in risus iaculis pretium. Mauris finibus tristique ullamcorper.
                        <fo:block text-align="justify" space-after="0.5cm">                           
                            Cras vitae felis nisi. Maecenas non metus iaculis, laoreet dolor sed, fringilla diam. Morbi posuere pretium faucibus. Morbi sed suscipit lorem. Aenean a mauris eu metus iaculis tristique. Integer tincidunt congue sapien non malesuada. Suspendisse felis eros, aliquam ac vestibulum vel, egestas non nulla.
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