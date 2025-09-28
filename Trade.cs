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
    public string OfferedItemId;
    public string RequestedItemId;
    public tradeStatus Status;

    public Trade(int id, string fromUserEmail, string toUserEmail, string offeredItemId, string requestedItemId)
    {
        Id = id;
        FromUserEmail = fromUserEmail;
        ToUserEmail = toUserEmail;
        OfferedItemId = offeredItemId;
        RequestedItemId = requestedItemId;
        Status = tradeStatus.pending; //new trades starts with pending
    }

}

enum tradeStatus
{
    none,
    pending,
    accepted,
    denied,
}