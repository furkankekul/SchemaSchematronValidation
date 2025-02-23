<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:inv="urn:oasis:names:specification:ubl:schema:xsd:Invoice-2"
    xmlns:cbc="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2"
    xmlns:cac="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2"
    xmlns:ext="urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2">

    <!-- HTML çıktısını tanımlıyoruz -->
    <xsl:output method="html" encoding="UTF-8" indent="yes"/>

    <!-- Ana Template -->
    <xsl:template match="/">
        <html>
            <head>
                <title>Fatura Detayları</title>
                <style>
                    body {
                        font-family: Arial, sans-serif;
                        background-color: #f4f4f9;
                        color: #333;
                        padding: 20px;
                    }
                    h1 {
                        text-align: center;
                        color: #444;
                    }
                    table {
                        width: 80%;
                        margin: 0 auto;
                        border-collapse: collapse;
                        box-shadow: 0 2px 5px rgba(0, 0, 0, 0.2);
                        background: white;
                    }
                    th, td {
                        padding: 10px;
                        border: 1px solid #ddd;
                        text-align: left;
                    }
                    th {
                        background-color: #f8f8f8;
                        color: #555;
                    }
                    tr:nth-child(even) {
                        background-color: #f9f9f9;
                    }
                </style>
            </head>
            <body>
                <h1>Fatura Bilgileri</h1>
                <table>
                    <tr>
                        <th>Fatura Numarası</th>
                        <td><xsl:value-of select="/inv:Invoice/cbc:ID"/></td>
                    </tr>
                    <tr>
                        <th>UUID</th>
                        <td><xsl:value-of select="/inv:Invoice/cbc:UUID"/></td>
                    </tr>
                    <tr>
                        <th>Fatura Tarihi</th>
                        <td><xsl:value-of select="/inv:Invoice/cbc:IssueDate"/></td>
                    </tr>
                    <tr>
                        <th>Fatura Saati</th>
                        <td><xsl:value-of select="/inv:Invoice/cbc:IssueTime"/></td>
                    </tr>
                    <tr>
                        <th>Fatura Türü</th>
                        <td><xsl:value-of select="/inv:Invoice/cbc:InvoiceTypeCode"/></td>
                    </tr>
                    <tr>
                        <th>Para Birimi</th>
                        <td><xsl:value-of select="/inv:Invoice/cbc:DocumentCurrencyCode"/></td>
                    </tr>
                    <tr>
                        <th>UBL Versiyonu</th>
                        <td><xsl:value-of select="/inv:Invoice/cbc:UBLVersionID"/></td>
                    </tr>
                    <tr>
                        <th>Özel Versiyon</th>
                        <td><xsl:value-of select="/inv:Invoice/cbc:CustomizationID"/></td>
                    </tr>
                    <tr>
                        <th>Profil Türü</th>
                        <td><xsl:value-of select="/inv:Invoice/cbc:ProfileID"/></td>
                    </tr>
                    <tr>
                        <th>Notlar</th>
                        <td>
                            <xsl:for-each select="/inv:Invoice/cbc:Note">
                                <div><xsl:value-of select="."/></div>
                            </xsl:for-each>
                        </td>
                    </tr>
                    <tr>
                        <th>Ek Belge Referansı</th>
                        <td>
                            <xsl:for-each select="/inv:Invoice/cac:AdditionalDocumentReference">
                                <div>
                                    <strong>Doküman ID:</strong> <xsl:value-of select="cbc:ID"/><br/>
                                    <strong>Doküman Türü:</strong> <xsl:value-of select="cbc:DocumentType"/><br/>
                                    <strong>Doküman Tarihi:</strong> <xsl:value-of select="cbc:IssueDate"/>
                                </div>
                            </xsl:for-each>
                        </td>
                    </tr>
                </table>
            </body>
        </html>
    </xsl:template>

</xsl:stylesheet>