﻿namespace SafeArgon2
{
    internal interface IArgon2PseudoRands
    {
        ulong PseudoRand(int segment, int prevLane, int prevOffset);
    }
}
