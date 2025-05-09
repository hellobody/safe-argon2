﻿namespace SafeArgon2
{
    internal class Argon2dCore : Argon2Core
    {
        internal class PseudoRands : IArgon2PseudoRands
        {
            private readonly Argon2Lane[] _lanes;

            public PseudoRands(Argon2Lane[] lanes)
            {
                _lanes = lanes;
            }

            public ulong PseudoRand(int segment, int prevLane, int prevOffset)
            {
                var mem = _lanes[prevLane][prevOffset];

                return mem.Array[mem.Offset + 0];
            }
        }

        public Argon2dCore(int hashSize) : base(hashSize) {}

        public override int Type => 0;

        internal override IArgon2PseudoRands GenerateState(Argon2Lane[] lanes, int segmentLength, int pass, int lane, int slice)
        {
            return new PseudoRands(lanes);
        }
    }
}
