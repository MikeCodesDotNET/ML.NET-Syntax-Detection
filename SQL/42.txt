update u
set u.assid = s.assid
from ud u
    inner join sale s on
        u.id = s.udid