namespace FreeCellSolitaire.Core.GameModels;

public class Track 
{
    public Track(string notation)
    {
        this.Notation = notation;
    }

    public override string ToString()
    {
        return this.Notation;
    }

    public Track Clone()
    {
        return new Track(this.Notation);
    }

    public string Notation { get; set; }
}