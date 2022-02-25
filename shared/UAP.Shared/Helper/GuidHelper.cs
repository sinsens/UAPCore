using System;
using System.Security.Cryptography;

namespace UAP.Shared.Helper
{
    /// <summary>
    /// GUID 帮助类
    /// </summary>
    public static class GuidHelper
    {

        /// <summary>
        /// 获取顺序 GUID
        /// </summary>
        /// <param name="guidType">GUID类型</param>
        /// <returns></returns>
        public static Guid GetSequentialGuid(SequentialGuidType guidType = SequentialGuidType.SequentialAtEnd)
        {
            return SequentialGuidGenerator.NewSequentialGuid(guidType);
        }

        #region 顺序 GUID 生成
        /*
         * fork from https://www.codeproject.com/Articles/388157/GUIDs-as-fast-primary-keys-under-multiple-database
         * framework\src\Volo.Abp.EntityFrameworkCore.SqlServer->AbpEntityFrameworkCoreSqlServerModule.cs
         * Volo.Abp.Guid->SequentialGuidType.cs
         */
        /// <summary>
        /// 序列化类型，不同数据库使用不同的类型，默认为 SQLServer 用的 SequentialGuidType.SequentialAtEnd
        /// 参考 https://www.codeproject.com/Articles/388157/GUIDs-as-fast-primary-keys-under-multiple-database
        /// </summary>
        public enum SequentialGuidType
        {
            /// <summary>
            /// MySQL Or PostgreSQL
            /// </summary>
            SequentialAsString,
            /// <summary>
            /// Oracle
            /// </summary>
            SequentialAsBinary,
            /// <summary>
            /// SQLServer
            /// </summary>
            SequentialAtEnd
        }

        /// <summary>
        /// 顺序 GUID 生成器
        /// </summary>
        public static class SequentialGuidGenerator
        {
            private static readonly RNGCryptoServiceProvider _rng = new RNGCryptoServiceProvider();

            /// <summary>
            /// 生成顺序GUID
            /// </summary>
            /// <param name="guidType">GUID类型：按数据库</param>
            /// <returns></returns>
            public static Guid NewSequentialGuid(SequentialGuidType guidType = SequentialGuidType.SequentialAtEnd)
            {
                byte[] randomBytes = new byte[10];
                _rng.GetBytes(randomBytes);

                long timestamp = DateTime.UtcNow.Ticks / 10000L;
                byte[] timestampBytes = BitConverter.GetBytes(timestamp);

                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(timestampBytes);
                }

                byte[] guidBytes = new byte[16];

                switch (guidType)
                {
                    case SequentialGuidType.SequentialAsString:
                    case SequentialGuidType.SequentialAsBinary:
                        Buffer.BlockCopy(timestampBytes, 2, guidBytes, 0, 6);
                        Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);

                        // If formatting as a string, we have to reverse the order
                        // of the Data1 and Data2 blocks on little-endian systems.
                        if (guidType == SequentialGuidType.SequentialAsString && BitConverter.IsLittleEndian)
                        {
                            Array.Reverse(guidBytes, 0, 4);
                            Array.Reverse(guidBytes, 4, 2);
                        }
                        break;

                    case SequentialGuidType.SequentialAtEnd:
                        Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 10);
                        Buffer.BlockCopy(timestampBytes, 2, guidBytes, 10, 6);
                        break;
                }

                return new Guid(guidBytes);
            }
        }
        #endregion
    }
}
