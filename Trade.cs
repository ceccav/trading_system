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
    public TradeStatus Status;

    public List<string> OfferedItemId = new List<string>();

    public Trade(int id, string fromUserEmail, string toUserEmail, string requestedItemId, List<string> offeredItemId)
    {
        Id = id;
        FromUserEmail = fromUserEmail;
        ToUserEmail = toUserEmail;
        OfferedItemId = offeredItemId;
        RequestedItemId = requestedItemId;
        Status = TradeStatus.pending; //new trades starts with pending
    }

    // TRY MAKE TRADE, vilka är alla parametrar den ska ta in.. logik
    public static bool tryMakeTrade(List<Item> items, List<Trade> trades, string fromUserEmail, string requestedItemId, List<string> offeredItemIds, int newTradeId)
    {
        //find the item id
        Item target = null;
        int i;
        for (i = 0; i < items.Count; i++)
        {
            if (items[i].ID == requestedItemId)
            {
                target = items[i];
                break;
            }
        }
        if (target == null) return false; //if there is no index matching return false
        if (target.OwnerEmail == fromUserEmail) return false; //if the index is the active users return false

        //validate offer

        List<string> offeredValid = new List<string>();
        for (i = 0; i < offeredItemIds.Count; i++)
        {
            string offeredId = offeredItemIds[i];
            Item offeredItem = null;

            int j;
            for (j = 0; j < items.Count; j++)
            {
                if (items[j].ID == offeredId)
                {
                    offeredItem = items[j];
                    break;
                }
            }

            if (offeredItem == null) continue;
            if (offeredItem.OwnerEmail != fromUserEmail) continue;
            if (offeredItem.ID == requestedItemId) continue;

            bool exists = false;
            int k;
            for (k = 0; k < offeredValid.Count; k++)
            {
                if (offeredValid[k] == offeredId)
                {
                    exists = true;
                    break;
                }

            }
            if (!exists) offeredValid.Add(offeredId);
        }

        if (offeredValid.Count == 0) return false;

        //create and add trade

        Trade t = new Trade(newTradeId, fromUserEmail, target.OwnerEmail, target.ID, offeredValid);
        trades.Add(t);
        return true;
    }

}

enum TradeStatus
{
    none,
    pending,
    accepted,
    denied,
}

