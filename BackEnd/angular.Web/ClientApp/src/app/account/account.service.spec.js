"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var account_service_1 = require("./account.service");
describe('AccountService', function () {
    beforeEach(function () { return testing_1.TestBed.configureTestingModule({}); });
    it('should be created', function () {
        var service = testing_1.TestBed.get(account_service_1.AccountService);
        expect(service).toBeTruthy();
    });
});
//# sourceMappingURL=account.service.spec.js.map