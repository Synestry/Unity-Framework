using System;
using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.Framework.Utils.Vector
{
    public struct IntVector3 : IEquatable<IntVector3>
    {
        public readonly int X;

        public readonly int Y;

        public readonly int Z;

        public IntVector3(int x = 0, int y = 0, int z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3 ToVector3()
        {
            return new Vector3(X, Y, Z);
        }

        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            return other is IntVector3 && Equals((IntVector3)other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = X;
                hashCode = (hashCode * 397) ^ Y;
                hashCode = (hashCode * 397) ^ Z;
                return hashCode;
            }
        }

        public bool Equals(IntVector3 other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override string ToString()
        {
            return "X: " + X + " Y: " + Y + " Z: " + Z;
        }

        public static IntVector3 operator +(IntVector3 a, IntVector3 b)
        {
            return new IntVector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static IntVector3 operator -(IntVector3 a, IntVector3 b)
        {
            return new IntVector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static IntVector3 operator -(IntVector3 a)
        {
            return new IntVector3(-a.X, -a.Y, -a.Z);
        }

        public static IntVector3 operator *(IntVector3 a, IntVector3 b)
        {
            return new IntVector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        public static IntVector3 Zero { get {return new IntVector3(0, 0, 0); } }

        public static IntVector3 One { get { return new IntVector3(1, 1, 1); } }

        public static IntVector3 Forward { get { return new IntVector3(0, 0, 1); } }

        public static IntVector3 Back { get { return new IntVector3(0, 0, -1); } }

        public static IntVector3 Up { get { return new IntVector3(0, 1, 0); } }

        public static IntVector3 Down { get { return new IntVector3(0, -1, 0); } }

        public static IntVector3 Left { get { return new IntVector3(-1, 0, 0); } }

        public static IntVector3 Right { get { return new IntVector3(1, 0, 0); } }

        public static List<IntVector3> Directions
        {
            get { return new List<IntVector3>() { Down, Up, Back, Forward, Left, Right }; }
        }
    }
}
