# Ohje https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/powershell
1. Valitse Data käynnistys projektiksi
2. Poista Kaikki tiedostot Entity kansiosta
3. Avaa package manager console
4. Valitse default projekti Data.Source
5. Aja komento
Scaffold-DbContext "Data Source=alfamereportingtest.database.windows.net;Initial Catalog=import_test;Integrated Security=False;User ID=xxxx;Password=yyyy;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" Microsoft.EntityFrameworkCore.SqlServer -Context ReportingContext -OutputDir Entity -Tables dbo.YammerHashtags -Force