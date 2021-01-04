using System;
using System.Buffers.Binary;
using System.IO;
using System.Text;

namespace Minter.Networking.Protocol
{
    public class MinecraftProtocolReader : BinaryReader
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public MinecraftProtocolReader(Stream input) : base(input) {}

        /// <summary>
        /// Reads string
        /// </summary>
        /// <returns></returns>
        public override string ReadString()
        {
            var length = ReadVarInt();
            var bytes = ReadBytes(length);

            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// Reads short value
        /// </summary>
        /// <returns></returns>
        public override short ReadInt16()
        {
            var value = base.ReadInt16();
            
            return BitConverter.IsLittleEndian
                ? BinaryPrimitives.ReverseEndianness(value)
                : value;
        }
        
        /// <summary>
        /// Reads ushort value
        /// </summary>
        /// <returns></returns>
        public override ushort ReadUInt16()
        {
            var value = base.ReadUInt16();
            
            return BitConverter.IsLittleEndian
                ? BinaryPrimitives.ReverseEndianness(value)
                : value;
        }
        
        /// <summary>
        /// Reads int value
        /// </summary>
        /// <returns></returns>
        public override int ReadInt32()
        {
            var value = base.ReadInt32();
            
            return BitConverter.IsLittleEndian
                ? BinaryPrimitives.ReverseEndianness(value)
                : value;
        }

        /// <summary>
        /// Reads long value
        /// </summary>
        /// <returns></returns>
        public override long ReadInt64()
        {
            var value = base.ReadInt16();
            
            return BitConverter.IsLittleEndian
                ? BinaryPrimitives.ReverseEndianness(value)
                : value;
        }
        
        /// <summary>
        /// Reads VarInt
        /// </summary>
        /// <returns></returns>
        /// <exception cref="IOException"></exception>
        public int ReadVarInt()
        {
            var value = 0;
            var size = 0;

            int b;

            while (((b = ReadByte()) & 0x80) == 0x80)
            {
                value |= (b & 0x7F) << (size++ * 7);

                if (size > 5)
                {
                    throw new IOException("VarInt is too long");
                }
            }

            return value | ((b & 0x7F) << (size * 7));
        }
    }
}
