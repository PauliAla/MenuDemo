﻿SELECT Ruokalistat FROM RavintolaLista
INNER JOIN RavintolaRuokalistat ON Ravintolalista.Id=RavintolaRuokalistat.RavintolaId
INNER JOIN Ruokalistat ON Ruokalistat.RavintolaId=RavintolaRuokalistat.RuokalistaId
WHERE Ravintolalista.Id=1