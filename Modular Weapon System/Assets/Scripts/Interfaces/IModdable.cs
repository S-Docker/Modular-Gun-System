public interface IModdable<in T>
{
    void AddMod(T mod);
    void RemoveMod(T mod);
}
