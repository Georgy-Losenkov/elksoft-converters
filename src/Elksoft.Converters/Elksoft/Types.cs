// <copyright file="Types.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft
{
    /// <summary>
    /// Provides static fields for primitive types widely used in project.
    /// </summary>
    internal static class Types
    {
        /// <summary><c>typeof(<see cref="System.Boolean"/>)</c>.</summary>
        public static readonly Type Boolean;

        /// <summary><c>typeof(<see cref="System.Byte"/>)</c>.</summary>
        public static readonly Type Byte;

        /// <summary><c>typeof(<see cref="System.Byte"/>[])</c>.</summary>
        public static readonly Type Binary;

        /// <summary><c>typeof(<see cref="System.Char"/>)</c>.</summary>
        public static readonly Type Char;

        /// <summary><c>typeof(<see cref="System.DateOnly"/>)</c>.</summary>
        public static readonly Type DateOnly;

        /// <summary><c>typeof(<see cref="System.DateTime"/>)</c>.</summary>
        public static readonly Type DateTime;

        /// <summary><c>typeof(<see cref="System.DateTimeOffset"/>)</c>.</summary>
        public static readonly Type DateTimeOffset;

        /// <summary><c>typeof(<see cref="System.Decimal"/>)</c>.</summary>
        public static readonly Type Decimal;

        /// <summary><c>typeof(<see cref="System.Double"/>)</c>.</summary>
        public static readonly Type Double;

        /// <summary><c>typeof(<see cref="System.Guid"/>)</c>.</summary>
        public static readonly Type Guid;

#if NET7_0_OR_GREATER
        /// <summary><c>typeof(<see cref="System.Half"/>)</c>.</summary>
        public static readonly Type Half;
#endif

        /// <summary><c>typeof(<see cref="System.Int16"/>)</c>.</summary>
        public static readonly Type Int16;

        /// <summary><c>typeof(<see cref="System.Int32"/>)</c>.</summary>
        public static readonly Type Int32;

        /// <summary><c>typeof(<see cref="System.Int64"/>)</c>.</summary>
        public static readonly Type Int64;

#if NET7_0_OR_GREATER
        /// <summary><c>typeof(<see cref="System.Int128"/>)</c>.</summary>
        public static readonly Type Int128;

        /// <summary><c>typeof(<see cref="System.IntPtr"/>)</c>.</summary>
        public static readonly Type IntPtr;
#endif

        /// <summary><c>typeof(<see cref="System.Object"/>)</c>.</summary>
        public static readonly Type Object;

        /// <summary><c>typeof(<see cref="System.SByte"/>)</c>.</summary>
        public static readonly Type SByte;

        /// <summary><c>typeof(<see cref="System.Single"/>)</c>.</summary>
        public static readonly Type Single;

        /// <summary><c>typeof(<see cref="System.String"/>)</c>.</summary>
        public static readonly Type String;

        /// <summary><c>typeof(<see cref="System.TimeOnly"/>)</c>.</summary>
        public static readonly Type TimeOnly;

        /// <summary><c>typeof(<see cref="System.TimeSpan"/>)</c>.</summary>
        public static readonly Type TimeSpan;

        /// <summary><c>typeof(<see cref="System.UInt16"/>)</c>.</summary>
        public static readonly Type UInt16;

        /// <summary><c>typeof(<see cref="System.UInt32"/>)</c>.</summary>
        public static readonly Type UInt32;

        /// <summary><c>typeof(<see cref="System.UInt64"/>)</c>.</summary>
        public static readonly Type UInt64;

#if NET7_0_OR_GREATER
        /// <summary><c>typeof(<see cref="System.UInt128"/>)</c>.</summary>
        public static readonly Type UInt128;

        /// <summary><c>typeof(<see cref="System.UIntPtr"/>)</c>.</summary>
        public static readonly Type UIntPtr;
#endif

        static Types()
        {
#pragma warning disable SA1025 // Code should not contain multiple whitespace in a row
            Boolean         = typeof(Boolean);
            Byte            = typeof(Byte);
            Binary          = typeof(Byte[]);
            Char            = typeof(Char);
            DateOnly        = typeof(DateOnly);
            DateTime        = typeof(DateTime);
            DateTimeOffset  = typeof(DateTimeOffset);
            Decimal         = typeof(Decimal);
            Double          = typeof(Double);
            Guid            = typeof(Guid);
            Int16           = typeof(Int16);
            Int32           = typeof(Int32);
            Int64           = typeof(Int64);
#if NET7_0_OR_GREATER
            Int128          = typeof(Int128);
            IntPtr          = typeof(IntPtr);
            Half            = typeof(Half);
#endif
            Object          = typeof(Object);
            SByte           = typeof(SByte);
            Single          = typeof(Single);
            String          = typeof(String);
            TimeOnly        = typeof(TimeOnly);
            TimeSpan        = typeof(TimeSpan);
            UInt16          = typeof(UInt16);
            UInt32          = typeof(UInt32);
            UInt64          = typeof(UInt64);
#if NET7_0_OR_GREATER
            UInt128         = typeof(UInt128);
            UIntPtr         = typeof(UIntPtr);
#endif
#pragma warning restore SA1025 // Code should not contain multiple whitespace in a row
        }
    }
}