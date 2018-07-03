import { getAsync } from "../helpers/apiHelper";
const endpoint = "/api/rank";

class RankApiService{
    constructor(){   
    }

    async GetPlayersProfitSorted(){
        return await getAsync(`${endpoint}/PlayersProfitSorted`)
    }

    async GetPlayersUserNameSorted(){
        return await getAsync(`${endpoint}/PlayersUserNameSorted`)
    }

    async GetPlayerNumberParts(){
        return await getAsync(`${endpoint}/PlayerNumberParts`)
    }
}
export default new RankApiService();