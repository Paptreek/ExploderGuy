namespace ExploderGuy
{
    public class PlayerBombAttributes
    {
        public int ActiveBombLimit { get; private set; }
        public int BlastRadius { get; private set; }
        public bool HasKick { get; private set; }
        public bool HasRemoteDetonator { get; private set; }

        public PlayerBombAttributes(int activeBombLimit, int blastRadius)
        {
            ActiveBombLimit = activeBombLimit;
            BlastRadius = blastRadius;
        }

        public void IncreaseBombLimit() => ActiveBombLimit++;
        public void IncreaseBlastRadius() => BlastRadius++;
        public void UnlockKick() => HasKick = true;
        public void UnlockRemoteDetonator() => HasRemoteDetonator = true;
    }
}
