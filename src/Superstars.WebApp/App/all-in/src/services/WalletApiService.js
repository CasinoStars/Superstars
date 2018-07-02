import { postAsync, getAsync, getAsyncNoJSON } from "../helpers/apiHelper";
const endpoint = "/api/wallet";

class WalletApiService {
    constructor() {
    }

    async AddCoins(model) {
        return await postAsync(endpoint, model);
    }

    async CreditPlayer(pot) {
        return await postAsync(`${endpoint}/${pot}/creditPlayer`);
    }

    async WithdrawInBankRoll(pot){
        return await postAsync(`${endpoint}/${pot}/withdraw`);
    }

    async GetFakeBalance(){
        return await getAsync(`${endpoint}/FakeBalance`);
    }

    async GetTrueBalance(){
        return await getAsync(`${endpoint}/TrueBalance`);
    }

    async UpdateCredit(credit) {
        return await postAsync(`${endpoint}/${credit}/updateCredit`);
    }

    async GetWalletAddress(){
        return  await getAsyncNoJSON(`${endpoint}/GetAddress`);
    }

    async Withdraw(model){
        return await postAsync(`${endpoint}/Withdraw`, model);
    }

    async GetBTCBankRoll(){
        return await getAsync(`${endpoint}/BTCBankRoll`);   
    }

    async GetFakeBankRoll(){
        return await getAsync(`${endpoint}/FakeBankRoll`);   
    }
}

export default new WalletApiService();