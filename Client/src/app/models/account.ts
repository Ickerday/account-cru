export interface IAccount {
    id: number;
    name?: string | undefined;
    availableFunds: number;
    balance: number;
    hasCard: boolean;
}

export class Account implements IAccount {
    id!: number;
    name?: string | undefined;
    availableFunds!: number;
    balance!: number;
    hasCard!: boolean;

    constructor(data?: IAccount) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    static fromJS(data: any): Account {
        data = typeof data === 'object' ? data : {};
        const result = new Account();
        result.init(data);
        return result;
    }

    init(data?: any) {
        if (data) {
            this.id = data['id'];
            this.name = data['name'];
            this.availableFunds = data['availableFunds'];
            this.balance = data['balance'];
            this.hasCard = data['hasCard'];
        }
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['id'] = this.id;
        data['name'] = this.name;
        data['availableFunds'] = this.availableFunds;
        data['balance'] = this.balance;
        data['hasCard'] = this.hasCard;
        return data;
    }
}
