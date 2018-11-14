import { postAsync, deleteAsync, getAsync, getboolasync, getAsyncNoJSON } from "../helpers/apiHelper";
import { get } from "https";
const endpoint = "/api/game";

 class GameApiService {
   constructor() {
}

async createGame(gametype) {
    return await postAsync(`${endpoint}/${gametype}`);
}

async deleteGame(gametype) {
    return await deleteAsync(`${endpoint}/deleteGame/${gametype}`);
}

async deleteYamsGame() {
    return await deleteAsync(`${endpoint}/deleteYamsGame`);
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

async UpdateStats(gametype, win) {
    return await postAsync(`${endpoint}/${gametype}/UpdateStats`, win);
}

async BetBTC(bet, gameType) {
    return await postAsync(`${endpoint}/${bet}/${gameType}/betBTC`);
}

async BetFake(bet, gameType) {
    return await postAsync(`${endpoint}/${bet}/${gameType}/betFake`);
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

async isInGame(gametype) {
    var data = await getAsync(`${endpoint}/isInGame/${gametype}`);
    return data;    
}

async gameEndUpdate(gametype, win) {
    console.log("PUTEPUTE");
    return await postAsync(`${endpoint}/GameEndUpdate/${gametype}/${win}`);
}

}

export default new GameApiService();