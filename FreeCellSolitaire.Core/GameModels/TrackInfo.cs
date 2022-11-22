namespace FreeCellSolitaire.Core.GameModels;

public class TrackInfo
{
    public TrackInfo(string notation, int hash)
    {
        this.Notation = notation;
        this.Hash = hash;
    }

    public override string ToString()
    {
        return this.Notation;
    }

    public int Hash { get; set; }
    public string Notation { get; set; }
}

