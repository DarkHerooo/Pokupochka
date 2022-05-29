using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLib.DB.Enums
{
    public enum StatusKey
    {
        Сonsidered = 1,
        Active = 2,
        Cancel = 3,
        Ended = 4,
        Stop = 5
    }

    // <Название статуса> = <Код статуса> (из БД)
}
