using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orchid.SeedWork.Core
{
    /// <summary>
    /// 考虑废弃，MS SQL已有SequeltialId()
    /// </summary>
    public static class SequentialGuid
    {
        public static Guid NewSequentialGuid()
        {
            var uid = Guid.NewGuid().ToByteArray();
            var binDate = BitConverter.GetBytes(DateTime.UtcNow.Ticks);

            var sequentialGuid = new byte[uid.Length];

            sequentialGuid[0] = uid[0];
            sequentialGuid[1] = uid[1];
            sequentialGuid[2] = uid[2];
            sequentialGuid[3] = uid[3];
            sequentialGuid[4] = uid[4];
            sequentialGuid[5] = uid[5];
            sequentialGuid[6] = uid[6];
            sequentialGuid[7] = (byte)(0xc0 | (0x0f & uid[7]));
            sequentialGuid[9] = binDate[0];
            sequentialGuid[8] = binDate[1];
            sequentialGuid[15] = binDate[2];
            sequentialGuid[14] = binDate[3];
            sequentialGuid[13] = binDate[4];
            sequentialGuid[12] = binDate[5];
            sequentialGuid[11] = binDate[6];
            sequentialGuid[10] = binDate[7];

            return new Guid(sequentialGuid);
        }
    }
}
