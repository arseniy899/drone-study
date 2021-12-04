from django.contrib import admin
from learning.models import Test, LearningResults, UserTestAnswers, TestQuestion
from django.utils.translation import gettext_lazy as _


class TestQuestionsInline(admin.TabularInline):
    model = Test.questions.through
    extra = 0


class TestAdmin(admin.ModelAdmin):
    fields = ('name', 'description')
    list_display = ('name',)
    inlines = [TestQuestionsInline]


class UserTestAnswersInline(admin.TabularInline):
    fields = ('test_question', 'answer_correctly', )
    model = UserTestAnswers
    can_delete = False
    extra = 0


class LearningResultsAdmin(admin.ModelAdmin):
    fields = ('test', 'users')
    list_display = ('test_name', 'users')
    inlines = [UserTestAnswersInline]

    @admin.display(description=_('Название теста'))
    def test_name(self, obj):
        return obj.test.name


admin.site.register(Test, TestAdmin)
admin.site.register(LearningResults, LearningResultsAdmin)
