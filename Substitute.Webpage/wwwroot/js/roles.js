$(function onRolesStart() {
    function SelectOption(name, value) {
        this.name = name;
        this.value = value;
    }

    function RoleModel(role) {
        var self = this;
        this.name = ko.observable(role.Name);
        this.accessLevel = ko.observable(role.AccessLevel);
        this.accessLevels = ko.observableArray([new SelectOption('User', 3), new SelectOption('Moderator', 2), new SelectOption('Administrator', 1)]);
        this.originalAccessLevel = ko.observable(accessLevel);
        this.hasChanged = ko.computed(function hasChanged() {
            return self.originalAccessLevel() != self.accessLevel();
        });
    };

    function RolesViewModel() {
        this.Roles = ko.observableArray([]);
    };
});