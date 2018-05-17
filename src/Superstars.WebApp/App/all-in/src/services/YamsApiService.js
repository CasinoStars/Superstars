import { postAsync } from "../helpers/apiHelper";
const endpoint = "/api/yams";

class YamsApiService {
    constructor() {
        
    }

async RollDices(selectedDices) {
    return await postAsync(endpoint, selectedDices);
}

async CreateYamsPlayer(gameid, nbturn, dices, dicesvalue) {
    return await postAsync(endpoint,gameid, nbturn, dices, dicesvalue)
}
    }
export default new YamsApiService();