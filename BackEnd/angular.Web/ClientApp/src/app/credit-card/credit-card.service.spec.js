"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var book_service_1 = require("./book.service");
describe('BookService', function () {
    beforeEach(function () { return testing_1.TestBed.configureTestingModule({}); });
    it('should be created', function () {
        var service = testing_1.TestBed.get(book_service_1.BookService);
        expect(service).toBeTruthy();
    });
});
//# sourceMappingURL=book.service.spec.js.map