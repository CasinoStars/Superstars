import { getAsync } from "../helpers/apiHelper";
const endpoint = "/api/rank";

class RankApiService{
    constructor(){   
    }

    async GetPlayersProfitSorted(TrueOrFake){
        return await getAsync(`${endpoint}/${TrueOrFake}/PlayersProfitSorted`)
    }

    async GetPlayersUserNameSorted(TrueOrFake){
        return await getAsync(`${endpoint}/${TrueOrFake}/PlayersUserNameSorted`)
    }

    async GetPlayersYamsNumberParts(TrueOrFake){
        return await getAsync(`${endpoint}/${TrueOrFake}/PlayersYamsNumberParts`)
    }

    async GetPlayersBlackJackNumberParts(TrueOrFake){
        return await getAsync(`${endpoint}/${TrueOrFake}/PlayersBlackJackNumberParts`)
    }
}
export default new RankApiService();