from django.contrib import admin
from questions.models import Question, QuestionsAnswers, QuestionCategory
from django.utils.translation import gettext_lazy as _


class QuestionCategoryAdmin(admin.ModelAdmin):
    list_display = ('name', )
    search_fields = ('name', )

    def has_add_permission(self, request):
        return False

    def has_delete_permission(self, request, obj=None):
        return False

    def has_change_permission(self, request, obj=None):
        return False


class QuestionAnswersInline(admin.TabularInline):
    model = QuestionsAnswers
    extra = 0
    list_display = ('name', 'is_correct')


class QuestionsAdmin(admin.ModelAdmin):
    list_display = ('short_name', 'category')
    search_fields = ('short_name', 'category')
    inlines = [QuestionAnswersInline]

    @admin.display(description=_('Категория'))
    def category(self, obj):
        return obj.category.name


admin.site.register(Question, QuestionsAdmin)
admin.site.register(QuestionCategory, QuestionCategoryAdmin)
