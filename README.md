# SchemaSchematronValidation
Günümüzde elektronik haberleşmelerin çoğalması ile verilerimizin güvenli bir şekilde  gönderici noktada çalışan bir uygulamadan alıcı noktada çalışan bir uygulamaya gönderirken   
verilerimizi xml formatında iletiyorsak, bu formatta göndereceğimiz bir verinin, xml formatına özgü isimlendirilen şema ve şematron kontrolünün .Net Core 8.0 mimarisi C# Programlama dilinin System.Xml.Schema ve System.Xml.Xsl kütüphanesi kullanılarak
bu kontrollerin nasıl yapılacağı pratikte incelenmiştir.
Burada xml'verilerimiz gelir idaresi başkanlığının kendi sitesinde yayınlamış olduğu örnek e-Fatura, e-Arşiv-, e-İrsaliye belgeleri olup,
Şema kontrolleri yine gelir idaresi başkanlığının kendi sitesinde yayınlamış olduğu Invoice.xsd, DespatchAdvice.xsd, ApplicationResponse.xsd, ReceiptAdvice.xsd belgeleri kullanılarak şema kontrolleri yapılmıştır. --> belgelere -> packges/xsdrt/maindoc burdan ulaşabilirsiniz 
Şematron kontrolleri ise System.Xml.Xsl kütüphanesi xslt1.0 versiyonu desteklediği için xslt1.0 versiyonunda bir xslt yazılarak şematron kontrolleri yapılmıştır. Belgelere packges/schematron/BasicSchematron.xslt burdan ulaşabilirsiniz

yazılan uygulamaya containerization teknolojisi uygulanmış olup Dockerfile dosyası ve docker-compose.yaml dosyalarını yine projenin altında bulabilir ve inceleyebilirsiniz.
yazılan uygulamaya yakında container orchestration teknolojisi (kubernetes) uygulanıp ubuntu ortamında kubernetes üzerinde yük testi yapılıp sistem davranışları için grafana kurulup metrikler analiz edilecektir.
