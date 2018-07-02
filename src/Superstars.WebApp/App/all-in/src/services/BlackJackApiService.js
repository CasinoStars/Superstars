import { postAsync, getAsync, getAsyncNoJSON, deleteAsync, getAsyncNoJSONBoolean, getboolasync } from "../helpers/apiHelper";
import { get } from "http";
const endpoint = "/api/blackJack";

class BlackJackApiService {
    constructor() {
    }

    async CreateJackPlayer() {
        await postAsync(`${endpoint}/CreatePlayer`);
    }

    async CreateJackAiPlayer() {
        await postAsync(`${endpoint}/CreateAi`);
    }

    async DeleteJackAiPlayer() {
        await deleteAsync(`${endpoint}/DeleteAi`);
    }
    
    async InitPlayer() {
        await postAsync(`${endpoint}/InitPlayer`);
    }

    async InitIa() {
        await postAsync(`${endpoint}/InitAI`);
    }

    // async GetSecondPlayerCards() {
    //     var cards = await getAsyncNoJSON(`${endpoint}/GetSecondPlayerCards`) + '';
    //     var array = cards.split(',');
    //     return array;
    // }


    async GetPlayerCards() {
        var cards = await getAsyncNoJSON(`${endpoint}/GetPlayerCards`) + '';
        var array = cards.split(',');
        return array;
    }

    async GetAiCards() {
        var cards = await getAsyncNoJSON(`${endpoint}/GetAiCards`) + '';
        var array = cards.split(',');
        return array;
    }

    async HitPlayer() {
        await postAsync(`${endpoint}/HitPlayer`);
    }

    // async HitPlayerSecondCards() {
    //     await postAsync(`${endpoint}/HitPlayerSecondCards`);
    // }

    refreshAiturn() {
        return getAsyncNoJSON(`${endpoint}/refreshAiturn`);
    }

    //  CanSplitPlayer() {
    //     var value = getAsync(`${endpoint}/canSplitPlayer`);
    //     var toto = Boolean(value);
    //     return toto;        
    // }

    // async SplitPlayer() {
    //     var value = postAsync(`${endpoint}/SplitPlayer`)
    //     return value;
    // }

    //  HasSplit() {       
    //     let value = getAsync(`${endpoint}/HasSplit`);
    //     console.log(value);
    //     var toto = Boolean(value);
    //     console.log(toto + "here");
    //     return toto;
    // }

     async StandPlayer() {
        var value = getAsync(`${endpoint}/StandPlayer`);
        var toto = Boolean(value);
        return toto;
    }

    // async StandPlayerSecondHand() {
    //     return getAsync(`${endpoint}/StandPlayer`);
    // }

    async PlayAI() {
        await postAsync(`${endpoint}/PlayAi`);
    }

    async GetTurn() {
        return await getAsync(`${endpoint}/getTurn`);
    }

    async getplayerHandValue() {
        var value = await getAsyncNoJSON(`${endpoint}/getplayerHandValue`);
        return value;
    }

    // async getplayerSecondHandValue() {
    //     var value = await getAsyncNoJSON(`${endpoint}/getplayerSecondHandValue`);
    //     return value;
    // }

    async getAiHandValue() {
        var value = await getAsyncNoJSON(`${endpoint}/getAiHandValue`);
        return value;
    }
}

export default new BlackJackApiService();