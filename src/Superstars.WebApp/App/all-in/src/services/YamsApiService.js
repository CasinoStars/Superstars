import { postAsync, getAsync } from "../helpers/apiHelper";
const endpoint = "/api/yams";

class YamsApiService {
    constructor(){

    }

    async RollDices(pseudo, myDices, selectedDices) {
        return await postAsync(`${endpoint}/${pseudo}/${myDices}/${selectedDices}`);
    }

    async CreateYamsPlayer(pseudo) {
        return await postAsync(`${endpoint}/${pseudo}`);
    }

    async GetPlayerDices(pseudo){
        var dices = await getAsync(`${endpoint}/${pseudo}`) + '';
        var array = dices.split('');
        return array;
    }
}
export default new YamsApiService();