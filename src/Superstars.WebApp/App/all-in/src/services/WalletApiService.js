import { postAsync, getAsync, getAsyncNoJSON } from "../helpers/apiHelper";
const endpoint = "/api/wallet";

class WalletApiService {
    constructor() {
    }

    async AddCoins(model) {
        return await postAsync(`${endpoint}/AddCoins`, model);
    }

    async CreditPlayerInBTC(pot) {
        return await postAsync(`${endpoint}/${pot}/creditBTCPlayer`);
    }

    async CreditPlayerInFake(pot) {
        return await postAsync(`${endpoint}/${pot}/creditFakePlayer`);
    }

    async WithdrawFakeBankRoll(pot) {
        return await postAsync(`${endpoint}/${pot}/withdrawFakeBank`);
    }

    async WithdrawBTCBankRoll(pot) {
        return await postAsync(`${endpoint}/${pot}/withdrawBTCBank`);
    }

    async GetFakeBalance() {
        return await getAsync(`${endpoint}/FakeBalance`);
    }

    async GetTrueBalance() {
        return await getAsync(`${endpoint}/TrueBalance`);
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