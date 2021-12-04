from django.contrib import admin
from learning.models import Test, LearningResults
from django.utils.translation import gettext_lazy as _


class TestQuestionsInline(admin.TabularInline):
    model = Test.questions.through
    extra = 0


class TestAdmin(admin.ModelAdmin):
    fields = ('name', )
    list_display = ('name',)
    inlines = [TestQuestionsInline]


class LearningResultsAdmin(admin.ModelAdmin):
    list_display = ('test_name',)

    @admin.display(description=_('Название теста'))
    def test_name(self, obj):
        return obj.test.name

    def has_add_permission(self, request):
        return False

    def has_change_permission(self, request, obj=None):
        return False


admin.site.register(Test, TestAdmin)
admin.site.register(LearningResults, LearningResultsAdmin)
