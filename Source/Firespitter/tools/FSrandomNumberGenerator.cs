/*
Firespitter /L
Copyright 2013-2018, Andreas Aakvik Gogstad (Snjo)
Copyright 2018-2020, LisiasT

    Developers: LisiasT, Snjo

    This file is part of Firespitter.
*/
using System;

public class FSrandomNumberGenerator
{
    public static Random rnd = new Random();
    public static int rndInt(int min, int max)
    {
        return rnd.Next(min, max);
    }
}
