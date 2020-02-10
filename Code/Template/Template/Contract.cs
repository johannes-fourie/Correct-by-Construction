using System;
using System.Runtime.CompilerServices;

namespace CodeContract
{

    public static class Contract
    {
        public static Requirer Require(bool condition, string message)
        {
            if (!condition)
                throw new ConstactRequireException(message);
            else
                return new Requirer();
        }

        public static Ensurer Ensure(bool condition, string message)
        {
            if (!condition)
                throw new ConstactEnsureException(message);
            else
                return new Ensurer();
        }

        public static Invarianter Invariant(bool condition, string message)
        {
            if (!condition)
                throw new ConstactInvariantException(message);
            else
                return new Invarianter();
        }
    }

    public class Invarianter
    {
        public Invarianter Invariant(bool condition, string message)
        {
            if (!condition)
                throw new ConstactEnsureException(message);
            else
                return this;
        }
    }

    public class Ensurer
    {
        public Ensurer Ensure(bool condition, string message)
        {
            if (!condition)
                throw new ConstactEnsureException(message);
            else
                return this;
        }
    }

    public class Requirer
    {
        public Requirer Require(bool condition, string message)
        {
            if (!condition)
                throw new ConstactRequireException(message);
            else
                return this;
        }
    }

    public class ConstactRequireException : Exception
    {
        public ConstactRequireException(string message)
            : base(message)
        { }
    }

    public class ConstactEnsureException : Exception
    {
        public ConstactEnsureException(string message)
            : base(message)
        { }
    }

    public class ConstactInvariantException : Exception
    {
        public ConstactInvariantException(string message)
            : base(message)
        { }
    }
}