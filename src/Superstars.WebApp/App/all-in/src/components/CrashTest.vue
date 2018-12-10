<template>
    <div>
        <div class="row">
            <div class="col">
                <div class="piecontainer" id="pie-container">
                    <canvas id="pie-chart" class="chartjs" width="770px" height="385px">
                    </canvas>
                    <center>
                        x{{multi}}
                    </center>
                </div>
            </div>
        </div>
        <div class="col-3">
            <form @submit="toBet($event)">
                <h4 style="color: black; font-family: 'Courier New', sans-serif;"> MISE <span class="req">*</span></h4>
                <div class="onoffswitch">
                    <input type="checkbox" name="onoffswitch" class="onoffswitch-checkbox" id="myonoffswitch" v-model="moneyType">
                    <label class="onoffswitch-label" for="myonoffswitch">
                        <span class="onoffswitch-inner"></span>
                        <span class="onoffswitch-switch"></span>
                    </label>
                </div>
                <input style="margin-top: 10px; margin-bottom: 1%;" type="number" min=1 required v-model="bet" />
                <h4 style="color: black; font-family: 'Courier New', sans-serif;"> MULTIPLICATEUR <span class="req">*</span></h4>
                <input style="margin-top: 10px; margin-bottom: 1%;" type="number" min=1 required v-model="playerMulti" />
                <div style="margin-right: 42%;">
                    <button type="submit" class="btn btn-light">Confirmer</button>
                </div>
            </form>
        </div>
        <div class="component-box-player-list">
            <table class="playerlist-table table table-striped table-bordered table-condensed table-hover">
                <thead class="table-header">
                    <tr>
                        <th>Joueur</th>
                        <th class="text-right">X</th>
                        <th class="text-right">Mise</th>
                        <th class="text-right">Profit</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(e, index) of playersData" :key="index">
                        <td>{{e.userName}}</td>
                        <td class="text-right">{{e.multi}}</td>
                        <td class="text-right">{{e.bet.toLocaleString('en')}}</td>
                        <td class="text-right">{{(e.bet * multi).toLocaleString('en')}}</td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div>
            <div class="table-responsive">
                <table class="playerlist-stats-table table table-striped table-condensed">
                    <tbody>
                        <tr class="table-footer">
                            <td>Joueurs: {{playersData.length}} </td>
                            <td>Mises: {{totalBet}} bits</td>
                        </tr>
                    </tbody>
                </table>
                <div class="row">
                    <div class="table-responsive col">
                        <table class="playerlist-stats-table table table-striped table-condensed">
                            <tbody>
                                <tr class="table-footer">
                                    <td>En ligne: 0 </td>
                                    <td>Joueurs: 0 </td>
                                    <td>Mises: 0 bits</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script src="~/lib/signalr/signalr.js"></script>

<script>
    import {
        mapActions,
        mapGetters
    } from 'vuex';
    import Vue from 'vue';
    import GameApiService from '../services/GameApiService';
    import CrashApiService from '../services/CrashApiService';
    import Chart from 'chart.js';

    export default {
        data() {
            return {
                chart: null,
                i: null,
                scale: null,
                multi: 1,
                fn: null,
                connection: null,
                bet: null,
                playerMulti: null,
                moneyType: true,
                playersData: [],
                totalBet: 0,
            }
        },

        computed: {
            ...mapGetters(['BTCMoney']),
            ...mapGetters(['fakeMoney'])
        },

        async mounted() {

            var signalR = require("@aspnet/signalr");
            this.connection = new signalR.HubConnectionBuilder().withUrl("/SignalR").configureLogging(signalR.LogLevel
                .Error).build();
            this.connection.on("Newgame", async (ite) => {
                console.log(ite)
                this.totalBet
                this.chart.destroy();
                this.initializeChart();
                this.i = 0
                this.myLoop(ite);
                await this.RefreshBTC();
                await this.RefreshFakeCoins();

            });
            this.connection.on("Wait", async () => {
                console.log("wait")
                this.totalBet = 0;
                this.playersData = [];
                await this.RefreshBTC();
                await this.RefreshFakeCoins();


            });

            this.connection.on("Bet", async (data) => {
                this.getPlayers(data);
            });

            this.connection.start();

            this.initializeChart();
            this.i = 0;
            var ite = await CrashApiService.GetNextCrash();


        },
        beforeDestroy() {
            this.chart.destroy();
            this.connection.stop();
            this.i = 0
            console.log("youpiiiiiiiiiiiiiiiiiiiiiiii!!!!")
        },

        methods: {
            ...mapActions(['executeAsyncRequest']),
            ...mapActions(['RefreshFakeCoins']),
            ...mapActions(['RefreshBTC']),

            initializeChart() {
                this.chart = new Chart(document.getElementById("pie-chart"), {
                    // The type of chart we want to create
                    type: 'line',

                    // The data for our dataset
                    data: {
                        datasets: [{
                            borderColor: 'rgb(255, 99, 132)',
                            data: [],
                            showLine: false, // disable for a single dataset
                        }]
                    },

                    // Configuration options go here
                    options: {
                        showLine: false, // disable for a single dataset
                        elements: {
                            line: {
                                tension: 0, // disables bezier curves
                            }
                        },
                        scales: {
                            xAxes: [{
                                type: "linear",
                                ticks: {
                                    suggestedMin: 1,
                                    suggestedMax: 10
                                }
                            }],
                            yAxes: [{
                                type: "linear",
                                ticks: {
                                    suggestedMin: 1,
                                    suggestedMax: 2
                                }
                            }]
                        },
                        animation: {
                            duration: 0, // general animation time
                        },
                        hover: {
                            animationDuration: 0, // duration of animations when hovering an item
                        },
                        responsiveAnimationDuration: 0 // animation duration after a resize
                    }
                });
            },

            addData(chart, x, y) {
                chart.data.datasets.forEach((dataset) => {
                    dataset.data.push({
                        x: x,
                        y: y
                    });
                });
                chart.update();
            },

            myLoop(iteration) {
                this.fn = setInterval(() => {
                    this.addData(this.chart, this.i / 10 + 1, Math.exp(this.i / 100));
                    this.multi = Math.exp(this.i / 100);
                    this.multi = Math.round(this.multi * 100) / 100;
                    this.i++;
                    if (this.multi >= iteration) {
                        this.multi = iteration;
                        clearInterval(this.fn);
                    }
                }, 100);

            },

            async toBet(e) {
                e.preventDefault();
                var errors = [];
                this.errors = 0;
                if (this.moneyType === false) {
                    if (this.bet > 1000000)
                        errors.push("La mise maximum est de 1,000,000");
                    else if (this.bet <= 0)
                        errors.push("La mise doit être supérieur à 0");
                    else if (this.bet > this.fakeMoney)
                        errors.push("Vous n'avez pas cette somme sur votre compte");
                } else {
                    if (this.bet > 10000000)
                        errors.push("La mise maximum est de 10,000,000 bits");
                    else if (this.bet <= 0)
                        errors.push("La mise doit être supérieur à 0 bits");
                    else if (this.bet > this.BTCMoney)
                        errors.push("Vous n'avez pas cette somme sur votre compte");
                }
                this.errors = errors;
                if (errors.length == 0) {
                    try {
                        if (this.moneyType === false) {
                            await this.executeAsyncRequest(() => GameApiService.BetCrash(this.bet, this.playerMulti,
                                this.moneyType));
                            await this.RefreshFakeCoins();
                            this.success = 'Vous venez de parier: ' + this.bet + ' All`In Coins';
                        } else {
                            await this.executeAsyncRequest(() => GameApiService.BetCrash(this.bet, this.playerMulti,
                                this.moneyType));
                            await this.RefreshBTC();
                            this.success = 'Vous venez de parier: ' + this.bet + ' Bits';
                        }
                    } catch (error) {
                        console.log(error)
                    }
                }
            },

            getPlayers(data) {
                this.playersData.push(data)
                this.playersData.forEach(elements => {
                    this.totalBet += elements.bet;
                });
            }
        }
    }
</script>


<style lang="scss">
    .piecontainer {
        width: 800px;
        height: 500px;
    }

    .text-right {
        text-align: right;
    }

    tr {
        background-color: #f2f2f2;
    }

    td,
    th {
        border-bottom: 1px solid #dddddd;
        text-align: left;
        padding: 14px;
        font-family: 'Courier New', sans-serif;
        font-size: 21px;
        font-variant: small-caps;
        font-weight: bold;
    }

    th {
        background-color: #343a40;
        color: white;
    }

    .onoffswitch {
        position: relative;
        width: 90px;
        -webkit-user-select: none;
        -moz-user-select: none;
        -ms-user-select: none;
    }

    .onoffswitch-checkbox {
        display: none;
    }

    .onoffswitch-label {
        display: block;
        overflow: hidden;
        cursor: pointer;
        border: 2px solid #999999;
        border-radius: 20px;
    }

    .onoffswitch-inner {
        display: block;
        width: 200%;
        margin-left: -100%;
        transition: margin 0.3s ease-in 0s;
    }

    .onoffswitch-inner:before,
    .onoffswitch-inner:after {
        display: block;
        float: left;
        width: 50%;
        height: 30px;
        padding: 0;
        line-height: 30px;
        font-size: 14px;
        color: white;
        font-family: Trebuchet, Arial, sans-serif;
        font-weight: bold;
        box-sizing: border-box;
    }

    .onoffswitch-inner:before {
        content: "Bitcoin";
        padding-left: 10px;
        background-color: #FF9900;
        color: #FFFFFF;
    }

    .onoffswitch-inner:after {
        content: "All'in";
        padding-right: 10px;
        background-color: #373C42;
        color: #CDCED0;
        text-align: right;
    }

    .onoffswitch-switch {
        display: block;
        width: 18px;
        margin: 6px;
        background: #FFFFFF;
        position: absolute;
        top: 0;
        bottom: 0;
        right: 56px;
        border: 2px solid #999999;
        border-radius: 20px;
        transition: all 0.3s ease-in 0s;
    }

    .onoffswitch-checkbox:checked+.onoffswitch-label .onoffswitch-inner {
        margin-left: 0;
    }

    .onoffswitch-checkbox:checked+.onoffswitch-label .onoffswitch-switch {
        right: 0px;
    }
</style>