import { getAsync } from "../helpers/apiHelper";
const endpoint = "/api/rank";

class RankApiService{
    constructor(){   
    }

    async GetPseudoList(){
        return await getAsync(`${endpoint}/PseudoList`)
    }

    async GetPlayerProfit(){
        return await getAsync(`${endpoint}/PlayerProfit`)
    }
}
export default new RankApiService();