import { getAsync } from "../helpers/apiHelper";
const endpoint = "/api/rank";

class RankApiService{
    constructor(){   
    }

    async GetPlayersGlobalProfit(moneyTypeId) {
        return await getAsync(`${endpoint}/${moneyTypeId}/GetPlayersGlobalProfit`);
    }

    async GetPlayerStats(pseudo, moneyTypeId) {
        return await getAsync(`${endpoint}/${pseudo}/${moneyTypeId}/GetPlayerStats`);
    }

    async GetTotalMoneyWagered(moneyTypeId) {
        return await getAsync(`${endpoint}/${moneyTypeId}/GetTotalMoneyWagered`);
    }

    async GetNumberOfPlayers() {
        return await getAsync(`${endpoint}/GetNumberOfPlayers`);
    }
}
export default new RankApiService();