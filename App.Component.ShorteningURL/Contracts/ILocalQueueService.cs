namespace App.Component.ShorteningURL.Contracts
{
    public interface ILocalQueueService
    {
        string GetKey();
        void LoadKeysFromKGS();
        void ReturnKey(string key);
    }
}