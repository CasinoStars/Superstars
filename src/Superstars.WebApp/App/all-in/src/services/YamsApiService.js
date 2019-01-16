import { postAsync, getAsync, deleteAsync } from "../helpers/apiHelper";
const endpoint = "/api/yams";

class YamsApiService {
    constructor(){

    }

    async UpdateRealProfit(profit){
        return await postAsync(`${endpoint}/UpdateRealProfit`,profit)
    }

    async UpdateFakeProfit(profit){
        return await postAsync(`${endpoint}/UpdateFakeProfit`,profit)
    }

    async RollDices(selectedDices) {
        return await postAsync(`${endpoint}/Roll`, selectedDices);
    }

    async RollIaDices(dices) {
        return await postAsync(`${endpoint}/RollIa`, dices); 
    }

    async CreateYamsPlayer() {
        return await postAsync(`${endpoint}/createPlayer`);
    }

    async CreateYamsAiPlayer() {
        return await postAsync(`${endpoint}/createAI`);
    }

    async DeleteYamsPlayer() {
        return await deleteAsync(`${endpoint}/deleteYamsPlayer`);
    }

    async DeleteYamsAiPlayer() {
        return await deleteAsync(`${endpoint}/deleteAI`);
    }

    async GetPlayerDices() {
        var dices = await getAsync(`${endpoint}/getPlayerDices`) + '';
        var array = dices.split('');
        return array;
    }

    async GetIaDices() {
        var dices = await getAsync(`${endpoint}/getIaDices`) + '';
        var array = dices.split('');
        return array;
    }

    async GetTurn() {
        return await getAsync(`${endpoint}/getTurn`);
    }

    async GetFinalResult() {
        return await getAsync(`${endpoint}/GetFinalResult`);
    }

    async GetScore(){
        return await getAsync(`${endpoint}/GetScore`);
    }
}
export default new YamsApiService();