$.fn.extend({
    setValidator: function () {
        //создаем validator с дефолтным конфигом
        var validator = this.kendoValidator({
            errorTemplate: '<span class="error" data-toggle="tooltip" data-placement="top" title="#=message#" data-trigger="hover">*</span>',
            rules: {
                requiredif: function (input) {
                    var value = input.val();
                    if (input.is("[data-val-requiredif-otherpropertyname]") && (value == null || value.replace(/^\s+/g, '').length === 0)) {
                        var otherPropertyName = input.data('val-requiredif-otherpropertyname');
                        var otherPropertyExpectedValue = input.data('val-requiredif-otherpropertyvalue');
                        var otherPropertyElement = $('[name="' + otherPropertyName + '"]').first();
                        if (otherPropertyElement.length > 0) {
                            if (otherPropertyElement.is(':checkbox')) {
                                return !otherPropertyElement.is(':checked');
                            }
                            else {
                                var otherPropertyValue = otherPropertyElement.val();
                                if (otherPropertyValue == otherPropertyExpectedValue) {
                                    // Условия, при которых проверка не должна проводиться
                                    var doNotApplyConditionNames = input.data('val-requiredif-donotapplyconditionname');
                                    var doNotApplyConditionValues = input.data('val-requiredif-donotapplyconditionvalue');
                                    if (doNotApplyConditionNames instanceof Array) {
                                        for (var i in doNotApplyConditionNames) {
                                            var otherPropertyElementCondition = $('[name="' + doNotApplyConditionNames[i] + '"]').first();
                                            if (otherPropertyElementCondition.length > 0) {
                                                if (otherPropertyElementCondition.is(':checkbox')) {
                                                    var res = otherPropertyElementCondition.is(':checked');
                                                    if (res.toString() == doNotApplyConditionValues[i].toLowerCase()) {
                                                        return true;
                                                    }
                                                }
                                                else {
                                                    var otherPropertyValueCondition = otherPropertyElementCondition.val();
                                                    var res = (otherPropertyValueCondition == doNotApplyConditionValues[i]);
                                                    if (res) {
                                                        return true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else {
                                        var otherPropertyElementCondition = $('[name="' + doNotApplyConditionNames + '"]').first();
                                        if (otherPropertyElementCondition.length > 0) {
                                            if (otherPropertyElementCondition.is(':checkbox')) {
                                                var res = otherPropertyElementCondition.is(':checked');
                                                if (res.toString() == doNotApplyConditionValues.toLowerCase()) {
                                                    return true;
                                                }
                                            }
                                            else {
                                                var otherPropertyValueCondition = otherPropertyElementCondition.val();
                                                var res = (otherPropertyValueCondition == doNotApplyConditionValues);
                                                if (res) {
                                                    return true;
                                                }
                                            }
                                        }
                                    }
                                }
                                return !(otherPropertyValue == otherPropertyExpectedValue && (value == null || value.length == 0));
                            }
                        }
                        else {
                            return false;
                        }
                    }
                    return true;
                },
                requiredifnot: function (input) {
                    var value = input.val();
                    if (input.is("[data-val-requiredifnot-otherpropertyname]") && (value == null || value.replace(/^\s+/g, '').length === 0)) {
                        var otherPropertyName = input.data('val-requiredifnot-otherpropertyname');
                        var otherPropertyElement = $('[name="' + otherPropertyName + '"]').first();
                        if (otherPropertyElement.length > 0) {
                            var otherPropertyValue = otherPropertyElement.val();
                            return otherPropertyValue != null && otherPropertyValue.length > 0;
                        }
                        else {
                            return false;
                        }
                    }
                    return true;
                }
            },
            messages: {
                requiredif: function (input) {
                    return input.data('val-requiredif')
                },
                requiredifnot: function (input) {
                    return input.data('val-requiredifnot')
                }
            }
        }).data("kendoValidator");

        //активируем всплывающие подсказки bootstrap'a для валидации
        //this.tooltip({ selector: '[data-toggle=tooltip]' });

        return validator;
    }
});