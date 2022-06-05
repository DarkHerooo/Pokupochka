using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLib.DB.Enums
{
    public enum StatusKey
    {
        Considered = 1,
        Active = 2,
        Cancel = 3,
        Ended = 4,
        Stop = 5,
        InTheWay = 6,
        Delivered = 7
    }

    // <Название статуса> = <Код статуса> (из БД)
}
