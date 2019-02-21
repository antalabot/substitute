using System;
using System.Collections.Generic;
using System.Text;

namespace Substitute.Domain.Data
{
    public class Snowflake
    {
        #region Private readonly variables
        private readonly ushort _machineId;
        private readonly object _sequenceLock;
        #endregion

        #region Private variables
        private ushort _sequence;
        #endregion

        #region Constructor
        private Snowflake()
        {
            _machineId = 0;
            _sequenceLock = new object();
            _sequence = 0;
        }
        #endregion

        #region Singleton implementation
        private static Snowflake _instance;
        private static readonly object _instanceLock = new object();

        public static Snowflake Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new Snowflake();
                        }
                    }
                }

                return _instance;
            }
        }
        #endregion

        #region Public methods
        public ulong GetSnowflake()
        {
            ulong time = (ulong)new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            ushort sequence = GetNextSequenceNumber();
            return time << 22 | (((ulong)_machineId & 1023) << 12) | ((ulong)sequence & 4095);
        }
        #endregion

        #region Private helpers
        private ushort GetNextSequenceNumber()
        {
            ushort sequence;
            lock (_sequenceLock)
            {
                sequence = (ushort)(_sequence++ % 4096);
            }
            return sequence;
        }
        #endregion
    }
}
