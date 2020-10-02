"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var auth_interceptor_service_1 = require("./auth-interceptor.service");
describe('AuthInterceptorService', function () {
    beforeEach(function () { return testing_1.TestBed.configureTestingModule({}); });
    it('should be created', function () {
        var service = testing_1.TestBed.get(auth_interceptor_service_1.AuthInterceptorService);
        expect(service).toBeTruthy();
    });
});
//# sourceMappingURL=auth-interceptor.service.spec.js.map