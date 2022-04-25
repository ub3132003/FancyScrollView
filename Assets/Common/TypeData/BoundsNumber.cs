using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
struct BoundsNumber<T> where T : struct
{
    public  T value;
    public T shoe;
    public T cap;

    public BoundsNumber(T value, T shoeValue, T capValue) : this()
    {
        this.value = value;
        this.shoe = shoeValue;
        this.cap = capValue;
    }
    public static BoundsNumber<T> operator -(BoundsNumber<T> a, T b)
    {
        if (Greater(Sub(a.value, b), a.cap) || Less(Sub(a.value, b), a.shoe))
        {
            return a;
        }
        return new BoundsNumber<T>(Sub(a, b), a.shoe, a.cap);
    }

    public static BoundsNumber<T> operator +(BoundsNumber<T> a, T b)
    {
        if ( Greater(Sum(a.value, b), a.cap) || Less(Sum(a.value, b), a.shoe))
        {
            return a;
        }
        return new BoundsNumber<T>( Sum(a, b) ,a.shoe , a.cap);
    }

    public static BoundsNumber<T> operator *(BoundsNumber<T> a, T b)
    {
        throw new NotImplementedException();
    }
    public static BoundsNumber<T> operator /(BoundsNumber<T> a, T b)
    {
        throw new NotImplementedException();
    }
    public static BoundsNumber<T> operator %(BoundsNumber<T> a, T b)
    {
        throw new NotImplementedException();
    }

    private static bool Less(T a, T b)
    {
        return (dynamic)a < (dynamic)b;
    }

    private static bool Greater(T a, T b)
    {
        return (dynamic)a > (dynamic)b;
    }
    private static T Sum(T a, T b)
    {
        return (dynamic)a + (dynamic)b;
    }

    private static T Sub(T a, T b)
    {
        return (dynamic)a - (dynamic)b;
    }

    public static implicit operator T(BoundsNumber<T> value)
    {
        return value.value;
    }
}