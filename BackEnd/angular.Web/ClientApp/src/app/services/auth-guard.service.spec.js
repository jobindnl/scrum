"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var auth_guard_service_1 = require("./auth-guard.service");
describe('AuthGuardService', function () {
    beforeEach(function () { return testing_1.TestBed.configureTestingModule({}); });
    it('should be created', function () {
        var service = testing_1.TestBed.get(auth_guard_service_1.AuthGuardService);
        expect(service).toBeTruthy();
    });
});
//# sourceMappingURL=auth-guard.service.spec.js.map