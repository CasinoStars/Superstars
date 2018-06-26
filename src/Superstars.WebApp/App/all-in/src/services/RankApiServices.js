import { getAsync } from "../helpers/apiHelper";

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
