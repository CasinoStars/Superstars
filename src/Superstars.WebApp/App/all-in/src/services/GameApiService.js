import { postAsync, deleteAsync, getAsync } from "../helpers/apiHelper";
const endpoint = "/api/game";

 class GameApiService {
   constructor() {
}

async createGame(gameTypeId) {
    return await postAsync(`${endpoint}/${gameTypeId}`);
}

async getYamsPot() {
    return await getAsync(`${endpoint}/getYamsPot`);
}

async getBlackJackPot() {
    return await getAsync(`${endpoint}/getBlackJackPot`);
}

async DeleteAis() {
    return await deleteAsync(`${endpoint}/DeleteAis`);
}

async createAiUser() {
    return await postAsync(`${endpoint}/createAiUser`);
}

async UpdateStats(gameTypeId, win) {
    return await postAsync(`${endpoint}/${gameTypeId}/UpdateStats`, win);
}

async BetBTC(bet, gameTypeId) {
    return await postAsync(`${endpoint}/${bet}/${gameTypeId}/betBTC`);
}

async BetFake(bet, gameTypeId) {
    return await postAsync(`${endpoint}/${bet}/${gameTypeId}/betFake`);
}

async getWinsBlackJackPlayer() {
    return await getAsync(`${endpoint}/getwinsBlackJackPlayer`);
}

async getLossesBlackJackPlayer() {
    return await getAsync(`${endpoint}/getlossesBlackJackPlayer`);
}

async getWinsYamsPlayer() {
    return await getAsync(`${endpoint}/getwinsYamsPlayer`);
}

async getLossesYamsPlayer() {
    return await getAsync(`${endpoint}/getlossesYamsPlayer`);
}

async getTrueProfitPlayer() {
    return await getAsync(`${endpoint}/gettrueprofitplayer`);
}

async getFakeProfitPlayer() {
    return await getAsync(`${endpoint}/getfakeprofitplayer`);
}

}

export default new GameApiService();