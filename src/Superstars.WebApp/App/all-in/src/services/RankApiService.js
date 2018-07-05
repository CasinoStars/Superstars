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

    async GetPlayersYamsNumberParts(){
        return await getAsync(`${endpoint}/PlayersYamsNumberParts`)
    }

    async GetPlayersBlackJackNumberParts(){
        return await getAsync(`${endpoint}/PlayersBlackJackNumberParts`)
    }
}
export default new RankApiService();