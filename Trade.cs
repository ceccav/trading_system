//Receiver
//sender
//status
using System.ComponentModel.Design;
using System.Data.Common;

namespace App;

class Trade
{
    public int Id;
    public string FromUserEmail;
    public string ToUserEmail;
    public string RequestedItemId;
    public tradeStatus Status;

    public List<string> OfferedItemId = new List<string>();

    public Trade(int id, string fromUserEmail, string toUserEmail, string requestedItemId, List<string> offeredItemId)
    {
        Id = id;
        FromUserEmail = fromUserEmail;
        ToUserEmail = toUserEmail;
        OfferedItemId = offeredItemId;
        RequestedItemId = requestedItemId;
        Status = tradeStatus.pending; //new trades starts with pending
    }

    // TRY MAKE TRADE, vilka är alla parametrar den ska ta in.. logik

}

enum tradeStatus
{
    none,
    pending,
    accepted,
    denied,
}