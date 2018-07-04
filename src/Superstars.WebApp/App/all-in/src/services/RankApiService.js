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

    async GetPlayerNumberParts(userName){
        console.log(userName + "HERE");
        console.log(typeof userName);
        return await getAsync(`${endpoint}/PlayerNumberParts`, userName)
    }
}
export default new RankApiService();